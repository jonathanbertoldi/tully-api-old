﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tully.Api.Data;
using Tully.Api.Data.Seeders;
using Tully.Api.Identity;
using Tully.Api.Models;
using Tully.Api.Repositories;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(Configuration);
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddAutoMapper();

      services.AddTransient<DatabaseSeeder>();

      services.AddDbContext<TullyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddTransient<IdentityErrorDescriber, AppIdentityErrorDescriber>();

      services.AddIdentity<Usuario, Perfil>()
        .AddEntityFrameworkStores<TullyContext, int>()
        .AddDefaultTokenProviders();

      services.AddScoped<IRepository, Repository>();
      services.AddScoped<IUsuarioRepository, UsuarioRepository>();
      services.AddScoped<IDesafioRepository, DesafioRepository>();
      services.AddScoped<IFotoRepository, FotoRepository>();
      services.AddScoped<IRelacionamentoRepository, RelacionamentoRepository>();
      services.AddScoped<ITimelineRepository, TimelineRepository>();
      services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
      services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

      services.AddCors(config =>
      {
        config.AddPolicy("Web", builder =>
        {
          builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
      });

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TullyContext context, DatabaseSeeder databaseSeeder)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsProduction())
      {
        context.Database.Migrate();
      }

      databaseSeeder.Seed().Wait();

      app.UseFileServer();

      app.UseJwtBearerAuthentication(new JwtBearerOptions()
      {
        AutomaticAuthenticate = true,
        AutomaticChallenge = true,
        TokenValidationParameters = new TokenValidationParameters()
        {
          ValidIssuer = Configuration["Tokens:Issuer"],
          ValidAudience = Configuration["Tokens:Audience"],
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
          ValidateLifetime = true
        }
      });

      app.UseCors("Web");

      app.UseMvc();
    }
  }
}
