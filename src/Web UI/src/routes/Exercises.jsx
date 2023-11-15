import { useEffect, useState } from "react";
import { useGetExercisesQuery } from "../features/exercises/exercisesApiSlice";

const Exercises = () => {
    const [page, setPage] = useState(1);
    const [exercises, setExercises] = useState();
    const [pagination, setPagination] = useState();

    const { data, isLoading, isSuccess, isError, error } = useGetExercisesQuery(
        { pageNumber: page }
    );

    useEffect(() => {
        if (isSuccess) {
            setExercises(data.data);
            setPagination(data.pagination);
        }
    }, [isSuccess]);

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Exercises</h1>
            {isLoading ? (
                <div className="w-full h-screen flex justify-center items-center">
                    <span className="loading loading-spinner loading-lg"></span>
                </div>
            ) : (
                <div className="mt-5 flex flex-col gap-3">
                    {exercises?.map((ex) => (
                        <div key={ex.id} className="flex items-center gap-3">
                            <div className="w-16 h-16 bg-secondary rounded-full flex items-center justify-center text-black font-semibold">
                                {ex.name[0]}
                            </div>
                            <div className="flex flex-col">
                                <h5 className="font-semibold text-xl">
                                    {ex.name}
                                </h5>
                                <p className="text-gray-500 text-sm">
                                    {ex.muscleGroup}
                                </p>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Exercises;
