import WorkoutHistoryCard from "./WorkoutHistoryCard";

const WorkoutHistoryList = ({ workouts }) => {
    workouts.sort((a, b) => a.order - b.order);
    return (
        <div className="mt-3 flex flex-col gap-4">
            {workouts.map((workout) => (
                <WorkoutHistoryCard key={workout.id} workout={workout} />
            ))}
        </div>
    );
};

export default WorkoutHistoryList;
