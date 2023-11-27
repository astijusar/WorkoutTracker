import AddWorkoutExerciseModal from "../features/workouts/AddWorkoutExerciseModal";
import WorkoutExercise from "../features/workoutExercises/WorkoutExercise";
import {
    selectWorkout,
    updateWorkout,
} from "../features/workouts/workoutSlice";
import {
    useAddNewWorkoutTemplateMutation,
    useAddNewWorkoutExercisesMutation,
} from "../features/workouts/workoutsApiSlice";
import { useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

const CreateWorkoutTempalte = () => {
    const exerciseListmodalRef = useRef(null);
    const workout = useSelector(selectWorkout);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [addNewWorkout, { isLoading: isAddWorkoutLoading }] =
        useAddNewWorkoutTemplateMutation();
    const [addNewWorkoutExercises, { isLoading: isAddWorkoutExerciseLoading }] =
        useAddNewWorkoutExercisesMutation();

    const isLoading = isAddWorkoutLoading || isAddWorkoutExerciseLoading;
    const canSubmit =
        workout.exercises.length !== 0 &&
        !isAddWorkoutLoading &&
        !isAddWorkoutExerciseLoading &&
        !workout.exercises.some((ex) => ex.errors);

    useEffect(() => {
        const updatedWorkout = {
            name: "New Template",
            note: "",
            start: null,
            end: null,
            isTemplate: true,
            exercises: [],
        };
        dispatch(updateWorkout(updatedWorkout));
    }, []);

    const onCancel = () => {
        const freshWorkout = {
            name: "New workout",
            note: "",
            start: null,
            end: null,
            isTemplate: false,
            exercises: [],
        };
        dispatch(updateWorkout(freshWorkout));
        navigate("/profile");
    };

    const onNoteChange = (e) => {
        const newNote = e.target.value;
        if (newNote === undefined) return;
        const updatedWorkout = {
            ...workout,
            note: newNote,
        };
        dispatch(updateWorkout(updatedWorkout));
    };

    const onNameChange = (e) => {
        const newName = e.target.value;
        if (newName === undefined) return;
        const updatedWorkout = {
            ...workout,
            name: newName,
        };
        dispatch(updateWorkout(updatedWorkout));
    };

    const onTemplateSave = async () => {
        if (workout.exercises.length !== 0) {
            try {
                const result = await addNewWorkout(workout).unwrap();

                try {
                    await addNewWorkoutExercises({
                        workoutId: result.id,
                        exercises: workout.exercises,
                        isTemplate: true,
                    }).unwrap();
                    onCancel();
                } catch (err) {
                    console.error(
                        "Failed to create new workout template exercises"
                    );
                }
            } catch (err) {
                console.error("Failed to create new workout template");
            }
        } else {
            onCancel();
        }
    };

    return (
        <div className="mx-5 mt-5">
            <div className="flex justify-between items-center">
                <input
                    type="text"
                    className="input input-ghost ps-0 me-5 w-full text-3xl font-semibold placeholder-slate-200"
                    value={workout.name}
                    onChange={(e) => onNameChange(e)}
                />
                <button
                    className="btn btn-sm btn-secondary tracking-wider disabled:border-secondary disabled:text-slate-400"
                    onClick={() => onTemplateSave()}
                    disabled={!canSubmit}
                >
                    SAVE{" "}
                    {isLoading && (
                        <span className="loading loading-spinner loading-sm"></span>
                    )}
                </button>
            </div>
            <input
                type="text"
                placeholder="Workout note"
                className="input w-full mt-4 font-semibold"
                value={workout.note}
                onChange={(e) => onNoteChange(e)}
            />
            <div className="mt-5">
                {workout.exercises.map((ex) => (
                    <WorkoutExercise key={ex.id} exercise={ex} />
                ))}
            </div>
            <button
                className="btn btn-secondary mt-5 w-full tracking-wider"
                onClick={() => exerciseListmodalRef.current.showModal()}
            >
                ADD EXERCISE
            </button>
            <button
                className="btn btn-error mt-4 w-full hover:bg-red-500 tracking-wider"
                onClick={() => onCancel()}
            >
                CANCEL CREATION
            </button>
            <AddWorkoutExerciseModal modalRef={exerciseListmodalRef} />
        </div>
    );
};

export default CreateWorkoutTempalte;
