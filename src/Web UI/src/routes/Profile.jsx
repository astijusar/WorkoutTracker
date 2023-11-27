import WorkoutBarChart from "../features/workouts/WorkoutBarChart";

import { useSelector } from "react-redux";
import { selectCurrentUser } from "../features/auth/authSlice";
import { useGetWorkoutsQuery } from "../features/workouts/workoutsApiSlice";
import CenterSpinner from "../components/CenterSpinner";
import SettingsButton from "../features/profile/SettingsButton";

const Profile = () => {
    const userName = useSelector(selectCurrentUser);

    const {
        data: { data: workouts, pagination } = {},
        isLoading,
        isError,
    } = useGetWorkoutsQuery({ pageSize: 50, template: false });

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
                <>
                    <div className="mt-5 flex">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="currentColor"
                            className="w-16 h-16"
                        >
                            <path
                                fillRule="evenodd"
                                d="M18.685 19.097A9.723 9.723 0 0021.75 12c0-5.385-4.365-9.75-9.75-9.75S2.25 6.615 2.25 12a9.723 9.723 0 003.065 7.097A9.716 9.716 0 0012 21.75a9.716 9.716 0 006.685-2.653zm-12.54-1.285A7.486 7.486 0 0112 15a7.486 7.486 0 015.855 2.812A8.224 8.224 0 0112 20.25a8.224 8.224 0 01-5.855-2.438zM15.75 9a3.75 3.75 0 11-7.5 0 3.75 3.75 0 017.5 0z"
                                clipRule="evenodd"
                            />
                        </svg>
                        <div className="ms-3 flex flex-col justify-center">
                            <h5 className="text-xl font-semibold">
                                {userName}
                            </h5>
                            <p className="text-gray-400">
                                {pagination.totalCount}{" "}
                                {pagination.totalCount === 1
                                    ? "workout"
                                    : "workouts"}
                            </p>
                        </div>
                    </div>
                    <div className="mt-5 flex justify-between">
                        <h5 className="text-gray-400 tracking-widest">
                            DASHBOARD
                        </h5>
                    </div>
                    <div className="mt-3 h-96 flex justify-center">
                        <WorkoutBarChart workouts={workouts} />
                    </div>
                </>
            );
        }
    };

    return (
        <div className="mx-5">
            <SettingsButton />
            <h1 className="text-5xl font-semibold">Profile</h1>
            <Content />
        </div>
    );
};

export default Profile;
