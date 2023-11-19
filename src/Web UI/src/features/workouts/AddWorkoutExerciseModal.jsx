import Exercise from "../exercises/Exercise";
import CenterSpinner from "../../components/CenterSpinner";
import { useGetExercisesQuery } from "../exercises/exercisesApiSlice";
import { useState } from "react";

const AddWorkoutExerciseModal = ({ modalRef }) => {
    const [page, setPage] = useState(1);

    const {
        data: { data: exercises, pagination } = {},
        isLoading,
        isFetching,
    } = useGetExercisesQuery({ pageNumber: page });

    return (
        <dialog ref={modalRef} className="modal" id="addExerciseModal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <div className="p-3 flex flex-col gap-3">
                    <h1 className="font-semibold text-3xl">Exercises</h1>
                    <input
                        type="text"
                        placeholder="Search exercise..."
                        className="input w-full bg-neutral"
                    />
                    {isLoading || isFetching ? (
                        <CenterSpinner />
                    ) : (
                        <div className="flex flex-col">
                            {exercises.map((ex) => (
                                <Exercise
                                    key={ex.id}
                                    exercise={ex}
                                    addButton={true}
                                    listModalRef={modalRef}
                                />
                            ))}
                        </div>
                    )}
                </div>
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default AddWorkoutExerciseModal;
