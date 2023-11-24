import { useState } from "react";
import WorkoutTemplateList from "../features/workouts/WorkoutTemplateList";
import CenterSpinner from "../components/CenterSpinner";
import { useGetWorkoutsQuery } from "../features/workouts/workoutsApiSlice";
import { useNavigate } from "react-router-dom";

const WorkoutTemplate = () => {
    const [page, setPage] = useState(1);
    const navigate = useNavigate();

    const {
        data: { data: workouts, pagination } = {},
        isLoading,
        isFetching,
    } = useGetWorkoutsQuery({ pageNumber: page, template: true });

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Workout</h1>
            <h5 className="mt-5 text-sm text-gray-400 tracking-widest">
                QUICK START
            </h5>
            <button
                className="mt-3 w-full btn btn-secondary tracking-widest text-white"
                onClick={() => navigate("/create-workout")}
            >
                START AN EMPTY WORKOUT
            </button>
            <div className="mt-3 flex justify-between">
                <h5 className="text-sm text-gray-400 tracking-widest">
                    MY TEMPLATES
                </h5>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="currentColor"
                    className="w-6 h-6 hover:cursor-pointer"
                    onClick={() => navigate("/create-template")}
                >
                    <path
                        fillRule="evenodd"
                        d="M12 5.25a.75.75 0 01.75.75v5.25H18a.75.75 0 010 1.5h-5.25V18a.75.75 0 01-1.5 0v-5.25H6a.75.75 0 010-1.5h5.25V6a.75.75 0 01.75-.75z"
                        clipRule="evenodd"
                    />
                </svg>
            </div>
            {isLoading || isFetching ? (
                <CenterSpinner />
            ) : (
                <>
                    {workouts && workouts.length == 0 ? (
                        <h1 className="mt-5 text-3xl font-semibold text-center">
                            There are no templates!
                        </h1>
                    ) : (
                        <WorkoutTemplateList workouts={workouts} />
                    )}
                </>
            )}
        </div>
    );
};

export default WorkoutTemplate;
