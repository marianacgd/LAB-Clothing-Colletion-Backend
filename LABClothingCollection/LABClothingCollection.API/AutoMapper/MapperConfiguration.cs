using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Models;

namespace LABClothingCollection.API.AutoMapper
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            UsuarioMapping();
            ColecaoMapping();
        }

        private void UsuarioMapping()
        {
            CreateMap<UsuarioModel, UsuarioReadDTO>();
            CreateMap<UsuarioCreateDTO, UsuarioModel>();
            CreateMap<UsuarioUpdateDTO, UsuarioModel>();
        }

        private void ColecaoMapping()
        {
            CreateMap<ColecaoModel, ColecaoReadDTO>()
                .ForMember(dest => dest.Responsavel, opt => opt.MapFrom(src => src.Responsavel));
            CreateMap<ColecaoDTO, ColecaoModel>();
        }
    }
}
