using API.Models.DTOs;
using AutoMapper;

namespace API.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
        }
    }
}
