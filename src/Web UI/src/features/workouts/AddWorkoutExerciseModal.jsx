import CenterSpinner from "../../components/CenterSpinner";
import ExerciseList from "../exercises/ExerciseList";
import { useGetExercisesQuery } from "../exercises/exercisesApiSlice";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";

const AddWorkoutExerciseModal = ({ modalRef }) => {
    const [page, setPage] = useState(1);

    const {
        data: { data: exercises, pagination } = {},
        isLoading,
        isFetching,
    } = useGetExercisesQuery({ pageNumber: page });

    return (
        <dialog ref={modalRef} className="modal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <div className="p-3 flex flex-col gap-3">
                    <h1 className="font-semibold text-2xl">Exercises</h1>
                    <input
                        type="text"
                        placeholder="Search exercise..."
                        className="input w-full bg-neutral"
                    />
                    {isLoading || isFetching ? (
                        <CenterSpinner />
                    ) : (
                        <ExerciseList
                            exercises={exercises}
                            filter={false}
                            addButton={true}
                        />
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
