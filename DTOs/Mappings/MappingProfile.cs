

using AutoMapper;

using DESAFIO.Models;

namespace DESAFIO.DTOs.Mappings {

    // Mapamento
    public class MappingProfile : Profile {

        public MappingProfile() {

            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            //CreateMap<Cliente, UsuarioDTO>().ReverseMap();
            //CreateMap<Livro, LivroDTO>().ReverseMap();
        }


    }
}