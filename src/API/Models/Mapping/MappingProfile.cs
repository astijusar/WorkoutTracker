using API.Models.DTOs.Exercise;
using API.Models.DTOs.Workout;
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

            CreateMap<Workout, WorkoutDto>();
            CreateMap<ExerciseCreationDto, Exercise>();
            CreateMap<ExerciseUpdateDto, Exercise>().ReverseMap();
        }
    }
}
