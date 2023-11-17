import WorkoutBarChart from "../features/workouts/WorkoutBarChart";

import { useSelector, useDispatch } from "react-redux";
import { selectCurrentUser, logOut } from "../features/auth/authSlice";
import { useGetWorkoutsQuery } from "../features/workouts/workoutsApiSlice";
import CenterSpinner from "../components/CenterSpinner";

const Profile = () => {
    const dispatch = useDispatch();
    const userName = useSelector(selectCurrentUser);

    const {
        data: { data: workouts, pagination } = {},
        isLoading,
        isSuccess,
        isError,
        error,
    } = useGetWorkoutsQuery({ pageSize: 50 });

    return (
        <div className="mx-5">
            <div className="mt-3 flex justify-end">
                <a
                    className="hover:scale-125"
                    onClick={() => dispatch(logOut())}
                >
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="currentColor"
                        className="w-8 h-8"
                    >
                        <path
                            fillRule="evenodd"
                            d="M11.828 2.25c-.916 0-1.699.663-1.85 1.567l-.091.549a.798.798 0 01-.517.608 7.45 7.45 0 00-.478.198.798.798 0 01-.796-.064l-.453-.324a1.875 1.875 0 00-2.416.2l-.243.243a1.875 1.875 0 00-.2 2.416l.324.453a.798.798 0 01.064.796 7.448 7.448 0 00-.198.478.798.798 0 01-.608.517l-.55.092a1.875 1.875 0 00-1.566 1.849v.344c0 .916.663 1.699 1.567 1.85l.549.091c.281.047.508.25.608.517.06.162.127.321.198.478a.798.798 0 01-.064.796l-.324.453a1.875 1.875 0 00.2 2.416l.243.243c.648.648 1.67.733 2.416.2l.453-.324a.798.798 0 01.796-.064c.157.071.316.137.478.198.267.1.47.327.517.608l.092.55c.15.903.932 1.566 1.849 1.566h.344c.916 0 1.699-.663 1.85-1.567l.091-.549a.798.798 0 01.517-.608 7.52 7.52 0 00.478-.198.798.798 0 01.796.064l.453.324a1.875 1.875 0 002.416-.2l.243-.243c.648-.648.733-1.67.2-2.416l-.324-.453a.798.798 0 01-.064-.796c.071-.157.137-.316.198-.478.1-.267.327-.47.608-.517l.55-.091a1.875 1.875 0 001.566-1.85v-.344c0-.916-.663-1.699-1.567-1.85l-.549-.091a.798.798 0 01-.608-.517 7.507 7.507 0 00-.198-.478.798.798 0 01.064-.796l.324-.453a1.875 1.875 0 00-.2-2.416l-.243-.243a1.875 1.875 0 00-2.416-.2l-.453.324a.798.798 0 01-.796.064 7.462 7.462 0 00-.478-.198.798.798 0 01-.517-.608l-.091-.55a1.875 1.875 0 00-1.85-1.566h-.344zM12 15.75a3.75 3.75 0 100-7.5 3.75 3.75 0 000 7.5z"
                            clipRule="evenodd"
                        />
                    </svg>
                </a>
            </div>
            <h1 className="text-5xl font-semibold">Profile</h1>
            {isLoading ? (
                <CenterSpinner />
            ) : (
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
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="currentColor"
                            className="w-6 h-6"
                        >
                            <path
                                fillRule="evenodd"
                                d="M12 5.25a.75.75 0 01.75.75v5.25H18a.75.75 0 010 1.5h-5.25V18a.75.75 0 01-1.5 0v-5.25H6a.75.75 0 010-1.5h5.25V6a.75.75 0 01.75-.75z"
                                clipRule="evenodd"
                            />
                        </svg>
                    </div>
                    <div className="mt-3 h-96 flex justify-center">
                        <WorkoutBarChart />
                    </div>
                </>
            )}
        </div>
    );
};

export default Profile;
