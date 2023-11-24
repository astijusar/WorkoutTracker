import AddWorkoutExerciseModal from "../features/workouts/AddWorkoutExerciseModal";
import { useRef } from "react";

const CreateWorkoutTempalte = () => {
    const exerciseListmodalRef = useRef(null);

    return (
        <div className="mx-5 mt-5">
            <div className="flex justify-between items-center">
                <input
                    type="text"
                    className="input input-ghost ps-0 me-5 w-full text-3xl font-semibold placeholder-slate-200"
                    value={"New Template"}
                />
                <button className="btn btn-sm btn-secondary tracking-wider disabled:border-secondary disabled:text-slate-400">
                    SAVE
                </button>
            </div>
            <input
                type="text"
                placeholder="Workout note"
                className="input w-full mt-4 font-semibold"
            />
            <div className="mt-5">
                {/*workout.exercises.map((ex) => (
                    <WorkoutExercise key={ex.id} exercise={ex} />
                ))*/}
            </div>
            <button
                className="btn btn-secondary mt-5 w-full tracking-wider"
                onClick={() => exerciseListmodalRef.current.showModal()}
            >
                ADD EXERCISE
            </button>
            <button
                className="btn btn-error mt-4 w-full hover:bg-red-500 tracking-wider"
                onClick={() => onWorkoutCancel()}
            >
                CANCEL CREATION
            </button>
            <AddWorkoutExerciseModal modalRef={exerciseListmodalRef} />
        </div>
    );
};

export default CreateWorkoutTempalte;
