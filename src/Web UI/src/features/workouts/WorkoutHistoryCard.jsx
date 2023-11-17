import moment from "moment";
import BestSet from "./BestSet";

const WorkoutHistoryCard = ({ workout }) => {
    const { name, start, end, workoutExercises } = workout;

    const duration = moment.duration(moment(end).diff(moment(start)));
    const durationString =
        duration.asHours() < 1
            ? `${duration.minutes()}m`
            : `${duration.hours()}h ${duration.minutes()}m`;

    return (
        <div className="card border border-gray-400">
            <div className="card-body p-3">
                <h2 className="card-title leading-none">{name}</h2>
                <h5 className="text-sm text-gray-400">
                    {moment(start).format("MMMM d")}
                </h5>
                <div className="mt-2 flex items-center">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 20 20"
                        fill="currentColor"
                        className="w-5 h-5"
                    >
                        <path
                            fillRule="evenodd"
                            d="M10 18a8 8 0 100-16 8 8 0 000 16zm.75-13a.75.75 0 00-1.5 0v5c0 .414.336.75.75.75h4a.75.75 0 000-1.5h-3.25V5z"
                            clipRule="evenodd"
                        />
                    </svg>
                    <p className="ms-2">{durationString}</p>
                </div>
                <div className="mt-2 flex justify-between">
                    <h2 className="card-title text-base leading-none">
                        Exercise
                    </h2>
                    <h2 className="card-title text-base leading-none">
                        Best set
                    </h2>
                </div>
                <div className="leading-none">
                    {workoutExercises.map((ex) => (
                        <div key={ex.id} className="flex justify-between">
                            <h5 className="text-sm text-gray-400">
                                {ex.sets.length} x {ex.exercise.name}
                            </h5>
                            <h5 className="text-sm text-gray-400">
                                <BestSet exercise={ex} />
                            </h5>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default WorkoutHistoryCard;
