import { useState } from "react";
import { useGetExercisesQuery } from "../features/exercises/exercisesApiSlice";
import { selectCurrentUserRoles } from "../features/auth/authSlice";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import CenterSpinner from "../components/CenterSpinner";
import ExerciseList from "../features/exercises/ExerciseList";

const Exercises = () => {
    const [page, setPage] = useState(1);
    const userRoles = useSelector(selectCurrentUserRoles);
    const navigate = useNavigate();

    const isAdmin = userRoles.includes("Admin");

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
            <div className="mt-5 flex justify-between">
                <h1 className="text-5xl font-semibold">Exercises</h1>
                {isAdmin && (
                    <button
                        className="btn btn-secondary font-bold tracking-widest text-slate-200 disabled:bg-secondary disabled:opacity-50 disabled:text-slate-200"
                        disabled={isLoading || isError}
                        onClick={() => navigate("/create-exercise")}
                    >
                        Add Exercise
                    </button>
                )}
            </div>
            <Content />
        </div>
    );
};

export default Exercises;
