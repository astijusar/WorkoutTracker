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
        isError,
    } = useGetWorkoutsQuery({ pageNumber: page, template: false });

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
        } else if (workouts && workouts.length !== 0) {
            return (
                <>
                    <div className="mt-7 flex justify-between">
                        <h5 className="text-sm text-gray-400 tracking-widest">
                            {moment(workouts[0].end)
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
            );
        } else {
            return (
                <h1 className="mt-10 text-3xl font-medium text-center">
                    No workouts completed!
                </h1>
            );
        }
    };

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">History</h1>
            {isLoading ? <CenterSpinner /> : <Content />}
        </div>
    );
};

export default History;
