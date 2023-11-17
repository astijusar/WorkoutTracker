using AutoMapper;
using Core.Models.DTOs.Exercise;
using Core.Models.DTOs.User;
using Core.Models.DTOs.Workout;
using Core.Models.DTOs.WorkoutExercise;
using Core.Models.DTOs.WorkoutExerciseSet;

namespace Core.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseCreationDto, Exercise>();
            CreateMap<ExerciseUpdateDto, Exercise>().ReverseMap();

            CreateMap<Workout, WorkoutDto>()
                .ForCtorParam(
                    nameof(WorkoutDto.Id),
                    options => options.MapFrom(src => src.Id)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.Name),
                    options => options.MapFrom(src => src.Name)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.Note),
                    options => options.MapFrom(src => src.Note)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.Start),
                    options => options.MapFrom(src => src.Start)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.End),
                    options => options.MapFrom(src => src.End)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.IsTemplate),
                    options => options.MapFrom(src => src.IsTemplate)
                )
                .ForCtorParam(
                    nameof(WorkoutDto.WorkoutExercises),
                    options => options.MapFrom(src => src.Exercises)
                )
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
