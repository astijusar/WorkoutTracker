import Stopwatch from "../features/workouts/Stopwatch";

const Workout = () => {
    return (
        <div className="mx-5 mt-5">
            <div className="flex justify-between items-center">
                <input
                    type="text"
                    placeholder="New workout"
                    className="input input-ghost ps-0 me-5 w-full text-3xl font-semibold placeholder-slate-200"
                />
                <button className="btn btn-sm btn-secondary tracking-wider">
                    FINISH
                </button>
            </div>
            <div className="mt-2 text-lg font-semibold">
                <Stopwatch />
            </div>
            <input
                type="text"
                placeholder="Workout note"
                className="input w-full mt-4"
            />
            <button className="btn btn-secondary mt-5 w-full tracking-wider">
                ADD EXERCISE
            </button>
            <button className="btn btn-error mt-4 w-full hover:bg-red-500 tracking-wider">
                CANCEL WORKOUT
            </button>
        </div>
    );
};

export default Workout;
