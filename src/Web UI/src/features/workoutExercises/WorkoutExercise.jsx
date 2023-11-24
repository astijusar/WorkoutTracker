import { useDispatch } from "react-redux";
import { removeExercise, updateExercise } from "../workouts/workoutSlice";
import { useRef, useState } from "react";

const WorkoutExercise = ({ exercise }) => {
    const [weightError, setWeightError] = useState(null);
    const [repsError, setRepsError] = useState(null);

    const dispatch = useDispatch();
    const deletionWarningModalRef = useRef(null);

    const onDelete = () => {
        dispatch(removeExercise({ id: exercise.id }));
    };

    const onWeightChange = (index, e) => {
        const newWeight = e.target.value;

        let parsedWeight = parseFloat(newWeight);
        if (!isNaN(parsedWeight) && parsedWeight >= 0 && parsedWeight <= 1000) {
            setWeightError(null);
            const updatedExercise = {
                ...exercise,
                sets: exercise.sets.map((set, i) =>
                    i === index ? { ...set, weight: parsedWeight } : set
                ),
                errors: false,
            };
            dispatch(updateExercise(updatedExercise));
        } else {
            setWeightError("Weight must be a valid number between 0 and 1000");
            const updatedExercise = {
                ...exercise,
                sets: exercise.sets.map((set, i) =>
                    i === index ? { ...set, weight: parsedWeight } : set
                ),
                errors: true,
            };
            dispatch(updateExercise(updatedExercise));
        }
    };

    const onRepsChange = (index, e) => {
        const newReps = e.target.value;

        let parsedReps = parseInt(newReps);
        if (!isNaN(parsedReps) && parsedReps >= 0 && parsedReps <= 1000) {
            setRepsError(null);
            const updatedExercise = {
                ...exercise,
                sets: exercise.sets.map((set, i) =>
                    i === index ? { ...set, reps: parsedReps } : set
                ),
                errors: false,
            };
            dispatch(updateExercise(updatedExercise));
        } else {
            setRepsError("Reps must be a valid number between 0 and 1000");
            const updatedExercise = {
                ...exercise,
                sets: exercise.sets.map((set, i) =>
                    i === index ? { ...set, reps: parsedReps } : set
                ),
                errors: true,
            };
            dispatch(updateExercise(updatedExercise));
        }
    };

    const onSetDone = (index, e) => {
        const status = e.target.checked;
        const updatedExercise = {
            ...exercise,
            sets: exercise.sets.map((set, i) =>
                i === index ? { ...set, done: status } : set
            ),
        };
        dispatch(updateExercise(updatedExercise));
    };

    const onAddSet = () => {
        const lastSet = exercise.sets[exercise.sets.length - 1];
        const newSet = lastSet
            ? { reps: lastSet.reps, weight: lastSet.weight, done: false }
            : { reps: 0, weight: 0, done: false };

        const updatedExercise = {
            ...exercise,
            sets: [...exercise.sets, newSet],
        };
        dispatch(updateExercise(updatedExercise));
    };

    const onRemoveSet = () => {
        if (exercise.sets.length === 1) {
            dispatch(removeExercise({ id: exercise.id }));
        } else {
            const updatedExercise = {
                ...exercise,
                sets: exercise.sets.slice(0, -1),
            };
            dispatch(updateExercise(updatedExercise));
        }
    };

    return (
        <div>
            <div className="flex justify-between mb-2">
                <h5 className="font-semibold text-secondary">
                    {exercise.name}
                </h5>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                    className="w-5 h-5 text-secondary me-4 hover:cursor-pointer"
                    onClick={() => onDelete()}
                >
                    <path
                        fillRule="evenodd"
                        d="M8.75 1A2.75 2.75 0 006 3.75v.443c-.795.077-1.584.176-2.365.298a.75.75 0 10.23 1.482l.149-.022.841 10.518A2.75 2.75 0 007.596 19h4.807a2.75 2.75 0 002.742-2.53l.841-10.52.149.023a.75.75 0 00.23-1.482A41.03 41.03 0 0014 4.193V3.75A2.75 2.75 0 0011.25 1h-2.5zM10 4c.84 0 1.673.025 2.5.075V3.75c0-.69-.56-1.25-1.25-1.25h-2.5c-.69 0-1.25.56-1.25 1.25v.325C8.327 4.025 9.16 4 10 4zM8.58 7.72a.75.75 0 00-1.5.06l.3 7.5a.75.75 0 101.5-.06l-.3-7.5zm4.34.06a.75.75 0 10-1.5-.06l-.3 7.5a.75.75 0 101.5.06l.3-7.5z"
                        clipRule="evenodd"
                    />
                </svg>
            </div>
            <table className="table">
                <thead>
                    <tr className="font-medium tracking-widest text-slate-200">
                        <th>SET</th>
                        <th className="ps-8">KG</th>
                        <th className="ps-6">REPS</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {exercise.sets.map((set, index) => (
                        <tr key={exercise.id + exercise.exerciseId + index}>
                            <td className="font-semibold text-lg">
                                {index + 1}
                            </td>
                            <td className="font-semibold text-lg ps-0">
                                <input
                                    type="number"
                                    min="0"
                                    max="1000"
                                    step="0.1"
                                    className="input w-20 h-8 text-center placeholder:font-semibold placeholder:text-md placeholder:text-slate-300 [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                                    value={set.weight}
                                    onChange={(e) => onWeightChange(index, e)}
                                />
                            </td>
                            <td className="font-semibold text-lg ps-0">
                                <input
                                    type="number"
                                    min="0"
                                    max="1000"
                                    step="0.1"
                                    className="input w-20 h-8 text-center placeholder:font-semibold placeholder:text-md placeholder:text-slate-300 [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                                    value={set.reps}
                                    onChange={(e) => onRepsChange(index, e)}
                                />
                            </td>
                            <td className="w-0">
                                <input
                                    type="checkbox"
                                    checked={set.done}
                                    className="checkbox checkbox-success"
                                    onChange={(e) => onSetDone(index, e)}
                                />
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div>
                {weightError && (
                    <div className="mt-2 text-error text-center">
                        {weightError}
                    </div>
                )}
                {repsError && (
                    <div className="mt-2 text-error text-center">
                        {repsError}
                    </div>
                )}
            </div>
            <div className="flex justify-center">
                <button
                    className="btn btn-ghost font-medium tracking-widest text-success"
                    onClick={() => onAddSet()}
                >
                    ADD SET
                </button>
                <button
                    className="btn btn-ghost font-medium tracking-widest text-error"
                    onClick={() => onRemoveSet()}
                >
                    REMOVE SET
                </button>
            </div>
        </div>
    );
};

export default WorkoutExercise;
