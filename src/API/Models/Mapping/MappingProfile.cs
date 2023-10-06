using API.Models.DTOs.Exercise;
using AutoMapper;

namespace API.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseCreationDto, Exercise>();
            CreateMap<ExerciseUpdateDto, Exercise>().ReverseMap();
        }
    }
}
