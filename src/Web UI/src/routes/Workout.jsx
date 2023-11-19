import AddWorkoutExerciseModal from "../features/workouts/AddWorkoutExerciseModal";
import WorkoutExercise from "../features/workoutExercises/WorkoutExercise";
import Stopwatch from "../features/workouts/Stopwatch";
import { useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { selectWorkout } from "../features/workouts/workoutSlice";
import { updateWorkout } from "../features/workouts/workoutSlice";
import moment from "moment";

const Workout = () => {
    const dispatch = useDispatch();
    const workout = useSelector(selectWorkout);
    const exerciseListmodalRef = useRef(null);
    const navigate = useNavigate();

    useEffect(() => {
        if (workout.start === null) {
            const updatedWorkout = {
                ...workout,
                start: moment().format(),
            };
            dispatch(updateWorkout(updatedWorkout));
        }
    }, []);

    const onWorkoutCancel = () => {
        const freshWorkout = {
            name: "New workout",
            note: null,
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

    return (
        <div className="mx-5 mt-5">
            <div className="flex justify-between items-center">
                <input
                    type="text"
                    className="input input-ghost ps-0 me-5 w-full text-3xl font-semibold placeholder-slate-200"
                    value={workout.name}
                    onChange={(e) => onNameChange(e)}
                />
                <button className="btn btn-sm btn-secondary tracking-wider">
                    FINISH
                </button>
            </div>
            <div className="mt-2 text-lg font-semibold">
                <Stopwatch start={workout.start} />
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
                onClick={() => onWorkoutCancel()}
            >
                CANCEL WORKOUT
            </button>
            <AddWorkoutExerciseModal modalRef={exerciseListmodalRef} />
        </div>
    );
};

export default Workout;
