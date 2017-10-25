using AutoMapper;
using Tully.Api.Models;
using Tully.Api.ViewModels.AdminViewModels;
using Tully.Api.ViewModels.DesafioViewModels;
using Tully.Api.ViewModels.FotoViewModels;
using Tully.Api.ViewModels.RelacionamentoViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Usuario, AdminViewModel>();
      CreateMap<Usuario, AdminUpdateViewModel>();

      CreateMap<AdminPostViewModel, Usuario>();
      CreateMap<AdminUpdateViewModel, Usuario>();

      CreateMap<Usuario, UsuarioViewModel>();
      CreateMap<Usuario, UsuarioUpdateViewModel>();

      CreateMap<UsuarioPostViewModel, Usuario>();
      CreateMap<UsuarioUpdateViewModel, Usuario>();

      CreateMap<Desafio, DesafioViewModel>();

      CreateMap<DesafioPostViewModel, Desafio>();

      CreateMap<Foto, FotoViewModel>()
        .ForMember(a => a.Curtidas, opt => opt.ResolveUsing<FotoCurtidasResolver>())
        .ForMember(a => a.Descurtidas, opt => opt.ResolveUsing<FotoDescurtidasResolver>());
      CreateMap<FotoPostViewModel, Foto>();

      CreateMap<Relacionamento, RelacionamentoViewModel>();
      CreateMap<RelacionamentoPostViewModel, Relacionamento>();
    }
  }
}
