using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Modelos;
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
            ModeloMapping();
        }

        private void UsuarioMapping()
        {
            CreateMap<UsuarioModel, UsuarioReadDTO>();
            CreateMap<UsuarioCreateDTO, UsuarioModel>()
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));

            CreateMap<UsuarioUpdateDTO, UsuarioModel>();
            CreateMap<UsuarioUpdateStatusDTO, UsuarioModel>();
        }

        private void ColecaoMapping()
        {
            CreateMap<ColecaoModel, ColecaoReadDTO>()
                .ForMember(dest => dest.Responsavel, opt => opt.MapFrom(src => src.Responsavel));
            
            CreateMap<ColecaoDTO, ColecaoModel>();
            CreateMap<ColecaoUpdateStatusDTO, ColecaoModel>();

        }

        private void ModeloMapping()
        {
            CreateMap<ColecaoModel, ModeloColecaoReadDTO>();

            CreateMap<ModeloModel, ModeloReadDTO>()
                .ForMember(dest => dest.Colecao, opt => opt.MapFrom(src => src.Colecao))                ;

            CreateMap<ModeloDTO, ModeloModel>();

        }
    }
}
