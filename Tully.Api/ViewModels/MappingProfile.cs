using AutoMapper;
using Tully.Api.Models;
using Tully.Api.ViewModels.AdminViewModels;

namespace Tully.Api.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, AdminViewModel>();

            CreateMap<AdminPostViewModel, Usuario>();
        }
    }
}
