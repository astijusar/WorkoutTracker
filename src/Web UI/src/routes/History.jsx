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
        isFetching,
    } = useGetWorkoutsQuery({ pageNumber: page, template: false });

    const content =
        workouts && workouts.length !== 0 ? (
            <>
                <div className="mt-7 flex justify-between">
                    <h5 className="text-sm text-gray-400 tracking-widest">
                        {moment(workouts[0].end).format("MMMM").toUpperCase()}
                    </h5>
                    <h5 className="text-sm text-gray-400">
                        {pagination.totalCount}{" "}
                        {pagination.totalCount === 1 ? "workout" : "workouts"}
                    </h5>
                </div>
                <WorkoutHistoryList workouts={workouts} />
            </>
        ) : (
            <h1 className="mt-10 text-3xl font-medium text-center">
                No workouts completed!
            </h1>
        );

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">History</h1>
            {isLoading || isFetching ? <CenterSpinner /> : content}
        </div>
    );
};

export default History;
