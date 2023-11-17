import { useState } from "react";
import { useGetExercisesQuery } from "../features/exercises/exercisesApiSlice";
import CenterSpinner from "../components/CenterSpinner";
import ExerciseList from "../features/exercises/ExerciseList";

const Exercises = () => {
    const [page, setPage] = useState(1);

    const {
        data: { data: exercises, pagination } = {},
        isLoading,
        isSuccess,
        isError,
        error,
    } = useGetExercisesQuery({ pageNumber: page });

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Exercises</h1>
            {isLoading ? (
                <CenterSpinner />
            ) : (
                <ExerciseList exercises={exercises} />
            )}
        </div>
    );
};

export default Exercises;
