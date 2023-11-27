import Markdown from "react-markdown";
import { useSelector } from "react-redux";
import { selectCurrentUserRoles } from "../auth/authSlice";
import { useDeleteExerciseMutation } from "./exercisesApiSlice";
import { useNavigate } from "react-router-dom";

const ExerciseInfoModal = ({ exercise, modalRef }) => {
    const userRoles = useSelector(selectCurrentUserRoles);
    const navigate = useNavigate();
    const [deleteExercise] = useDeleteExerciseMutation();

    const onDeleteClicked = async () => {
        try {
            await deleteExercise(exercise.id).unwrap();
        } catch {
            console.error("Failed to delete the exercise");
        }

        modalRef.current.close();
    };

    const onEditClicked = () => {
        navigate(`/create-exercise/${exercise.id}`);
    };

    const isAdmin = userRoles.includes("Admin");

    return (
        <dialog ref={modalRef} className="modal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <h3 className="font-bold text-2xl p-3">{exercise.name}</h3>
                <div className="flex justify-center items-center w-full bg-primary h-60">
                    <h1 className="font-bold text-3xl tracking-widest">
                        EXERCISE IMAGE
                    </h1>
                </div>
                <h3 className="font-bold text-xl p-3">Instructions</h3>
                {exercise.instructions ? (
                    <Markdown className="px-5 mb-3 space-y-3">
                        {exercise.instructions}
                    </Markdown>
                ) : (
                    <p className="ps-3 mb-3 font-thin">
                        There are no instructions.
                    </p>
                )}
                {isAdmin && (
                    <div className="p-3 mt-3 flex justify-center gap-3">
                        <button
                            className="btn btn-secondary w-40 tracking-wider"
                            onClick={() => onEditClicked()}
                        >
                            Edit
                        </button>
                        <button
                            className="btn btn-error w-40 hover:bg-red-500 tracking-wider"
                            onClick={() => onDeleteClicked()}
                        >
                            Delete
                        </button>
                    </div>
                )}
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default ExerciseInfoModal;
