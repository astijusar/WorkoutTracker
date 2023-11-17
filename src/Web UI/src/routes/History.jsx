import { useState } from "react";
import WorkoutHistoryList from "../features/workouts/WorkoutHistoryList";
import CenterSpinner from "../components/CenterSpinner";
import { useGetWorkoutsQuery } from "../features/workouts/workoutsApiSlice";
import moment from "moment";

const History = () => {
    const [page, setPage] = useState(1);

    const {
        data: { data: workouts, pagination } = {},
        isLoading,
        isSuccess,
        isError,
        error,
    } = useGetWorkoutsQuery({ pageNumber: page, template: false });

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">History</h1>
            {isLoading ? (
                <CenterSpinner />
            ) : (
                <>
                    <div className="mt-7 flex justify-between">
                        <h5 className="text-sm text-gray-400 tracking-widest">
                            {moment(workouts[0].start)
                                .format("MMMM")
                                .toUpperCase()}
                        </h5>
                        <h5 className="text-sm text-gray-400">
                            {pagination.totalCount}{" "}
                            {pagination.totalCount === 1
                                ? "workout"
                                : "workouts"}
                        </h5>
                    </div>
                    <WorkoutHistoryList workouts={workouts} />
                </>
            )}
        </div>
    );
};

export default History;
