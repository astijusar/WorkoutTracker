using AutoMapper;
using Repository.Models.DTOs.Exercise;
using Repository.Models.DTOs.User;
using Repository.Models.DTOs.Workout;
using Repository.Models.DTOs.WorkoutExercise;
using Repository.Models.DTOs.WorkoutExerciseSet;

namespace Repository.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseCreationDto, Exercise>();
            CreateMap<ExerciseUpdateDto, Exercise>().ReverseMap();

            CreateMap<Workout, WorkoutDto>()
                .ForMember(
                    dest => dest.WorkoutExercises,
                    opt => opt.MapFrom(w => w.Exercises)
                );
            CreateMap<WorkoutCreationDto, Workout>();
            CreateMap<WorkoutUpdateDto, Workout>().ReverseMap();

            CreateMap<WorkoutExercise, WorkoutExerciseDto>()
                .ForMember(
                    dest => dest.Sets,
                    opt => opt.MapFrom(w => w.Sets)
                )
                .ForMember(
                    dest => dest.Exercise,
                    opt => opt.MapFrom(w => w.Exercise)
                );
            CreateMap<WorkoutExerciseCreationDto, WorkoutExercise>()
                .ForMember(
                    dest => dest.Sets,
                    opt => opt.MapFrom(w => w.Sets)
                );
            CreateMap<WorkoutExerciseUpdateDto, WorkoutExercise>()
                .ForMember(
                    dest => dest.Sets,
                    opt => opt.MapFrom(w => w.Sets)
                );

            CreateMap<WorkoutExerciseSet, WorkoutExerciseSetDto>();
            CreateMap<WorkoutExerciseSetCreationDto, WorkoutExerciseSet>();
            CreateMap<WorkoutExerciseSetUpdateDto, WorkoutExerciseSet>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForCtorParam(
                    nameof(UserDto.UserName),
                    options => options.MapFrom(src => src.UserName)
                )
                .ForCtorParam(
                    nameof(UserDto.Email),
                    options => options.MapFrom(src => src.Email)
                )
                .ForCtorParam(
                    nameof(UserDto.UserId),
                    options => options.MapFrom(src => src.Id)
                )
                .ForAllMembers(options => options.Ignore());
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
