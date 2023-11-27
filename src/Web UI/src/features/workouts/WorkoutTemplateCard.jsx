import { useDeleteWorkoutMutation } from "./workoutsApiSlice.js";
import { updateWorkout } from "./workoutSlice.js";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useRef } from "react";
import moment from "moment";
import WorkoutTemplateEditModal from "./WorkoutTemplateEditModal.jsx";

const WorkoutTemplateCard = ({ workout }) => {
    const editModalRef = useRef(null);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [deleteTemplate] = useDeleteWorkoutMutation();

    const onDeleteClicked = async () => {
        try {
            await deleteTemplate({ id: workout.id }).unwrap();
        } catch (err) {
            console.error("Failed to delete the workout template");
        }
    };

    const onPerformClicked = () => {
        const workoutToPerform = {
            name: workout.name,
            note: workout.note,
            start: moment().format(),
            end: null,
            isTemplate: false,
            exercises: workout.workoutExercises.map((exercise, index) => ({
                id: index + 1,
                exerciseId: exercise.exercise.id,
                name: exercise.exercise.name,
                sets: exercise.sets.map((set) => ({
                    reps: set.reps,
                    weight: set.weight,
                    done: set.done,
                })),
                errors: false,
            })),
        };
        dispatch(updateWorkout(workoutToPerform));
        navigate(`/create-workout`);
    };

    return (
        <div className="card border border-gray-400">
            <div className="card-body p-3">
                <div className="flex justify-between">
                    <h2 className="card-title leading-none">{workout.name}</h2>
                    <div className="dropdown dropdown-left dropdown-hover">
                        <label tabIndex={0} className="">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 20 20"
                                fill="currentColor"
                                className="w-5 h-5"
                            >
                                <path d="M10 3a1.5 1.5 0 110 3 1.5 1.5 0 010-3zM10 8.5a1.5 1.5 0 110 3 1.5 1.5 0 010-3zM11.5 15.5a1.5 1.5 0 10-3 0 1.5 1.5 0 003 0z" />
                            </svg>
                        </label>
                        <ul
                            tabIndex={0}
                            className="dropdown-content z-[1] menu p-2 shadow bg-neutral border border-gray-400 rounded-lg w-40 font-semibold"
                        >
                            <li>
                                <a onClick={() => onPerformClicked()}>
                                    Perform
                                </a>
                            </li>
                            <li>
                                <a
                                    onClick={() =>
                                        editModalRef.current.showModal()
                                    }
                                >
                                    Edit
                                </a>
                            </li>
                            <li>
                                <a
                                    className="text-error"
                                    onClick={() => onDeleteClicked()}
                                >
                                    Delete
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="leading-none">
                    {workout.workoutExercises
                        .map((exercise) => ({
                            ...exercise,
                            sets: [...exercise.sets].sort(
                                (a, b) => a.order - b.order
                            ),
                        }))
                        .map((exercise) => (
                            <h5
                                key={exercise.id}
                                className="text-sm text-gray-400"
                            >
                                {`${exercise.sets.length} x ${exercise.exercise.name}`}
                            </h5>
                        ))}
                </div>
                <WorkoutTemplateEditModal
                    template={workout}
                    modalRef={editModalRef}
                />
            </div>
        </div>
    );
};

export default WorkoutTemplateCard;
