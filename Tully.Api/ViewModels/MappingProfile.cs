using AutoMapper;
using Tully.Api.Models;
using Tully.Api.ViewModels.AdminViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, AdminViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();

            CreateMap<AdminPostViewModel, Usuario>();
        }
    }
}
