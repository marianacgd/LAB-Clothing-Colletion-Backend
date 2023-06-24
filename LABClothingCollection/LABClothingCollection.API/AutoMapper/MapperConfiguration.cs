using AutoMapper;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Models;

namespace LABClothingCollection.API.AutoMapper
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            UsuarioMapping();
        }

        private void UsuarioMapping()
        {
            CreateMap<UsuarioModel, UsuarioReadDTO>();
            CreateMap<UsuarioCreateDTO, UsuarioModel>();
            CreateMap<UsuarioUpdateDTO, UsuarioModel>();
        }
    }
}
