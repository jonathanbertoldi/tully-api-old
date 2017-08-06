using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tully.Api.Data;
using Tully.Api.Data.Seed;
using Tully.Api.Models.User;

namespace Tully.Api
{
    public class Startup
    {
        private IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddDbContext<TullyContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<Usuario, Perfil>()
                .AddEntityFrameworkStores<TullyContext, int>()
                .AddDefaultTokenProviders();

            services.AddTransient<DatabaseSeeder>();

            services.AddCors(config =>
            {
                config.AddPolicy("Web", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddAuthentication();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseSeeder databaseSeeder)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                databaseSeeder.Seed().Wait();

                app.UseDeveloperExceptionPage();
            }

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _configuration["Tokens:Issuer"],
                    ValidAudience = _configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
                    ValidateLifetime = true
                }
            });

            app.UseCors("Web");

            app.UseMvc();
        }
    }
}
