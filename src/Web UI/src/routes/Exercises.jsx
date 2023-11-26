import { useState } from "react";
import { useGetExercisesQuery } from "../features/exercises/exercisesApiSlice";
import CenterSpinner from "../components/CenterSpinner";
import ExerciseList from "../features/exercises/ExerciseList";

const Exercises = () => {
    const [page, setPage] = useState(1);

    const {
        data: { data: exercises, pagination } = {},
        isLoading,
        isError,
    } = useGetExercisesQuery({ pageNumber: page });

    const Content = () => {
        if (isError) {
            return (
                <div className="mt-10">
                    <h5 className="text-3xl font-medium text-center">
                        Could not reach the server!
                    </h5>
                    <p className="mt-2 text-center text-xl">Try again later!</p>
                </div>
            );
        } else if (isLoading) {
            return <CenterSpinner />;
        } else {
            return (
                <ExerciseList
                    exercises={exercises}
                    addButton={false}
                    filter={false}
                />
            );
        }
    };

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Exercises</h1>
            <Content />
        </div>
    );
};

export default Exercises;
