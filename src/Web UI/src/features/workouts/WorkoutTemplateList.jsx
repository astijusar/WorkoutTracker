import WorkoutTemplateCard from "./WorkoutTemplateCard";

const WorkoutTemplateList = ({ workouts }) => {
    return (
        <div className="mt-3 flex flex-col gap-4">
            {workouts.map((workout) => (
                <WorkoutTemplateCard key={workout.id} workout={workout} />
            ))}
        </div>
    );
};

export default WorkoutTemplateList;
