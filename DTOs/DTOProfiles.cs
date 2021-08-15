using AutoMapper;
using PropertyManager.Models;

namespace PropertyManager.DTOs
{
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {
            CreateMap<Command, CommandReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
        }
    }
}