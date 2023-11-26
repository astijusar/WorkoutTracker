import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./index.css";
import Root from "./routes/Root.jsx";
import Profile from "./routes/Profile.jsx";
import WorkoutTemplate from "./routes/WorkoutTemplate.jsx";
import Exercises from "./routes/Exercises.jsx";
import History from "./routes/History.jsx";
import CreateWorkout from "./routes/CreateWorkout.jsx";

import { store } from "./app/store.js";
import { Provider } from "react-redux";
import Login from "./routes/Login.jsx";
import RequireAuth from "./features/auth/RequireAuth.jsx";
import Register from "./routes/Register.jsx";
import CreateWorkoutTempalte from "./routes/CreateWorkoutTemplate.jsx";
import { Analytics } from "@vercel/analytics/react";
import CreateExercise from "./routes/CreateExercise.jsx";
import RequireAdminRole from "./features/auth/RequireAdminRole.jsx";

const router = createBrowserRouter([
    {
        path: "/login",
        element: <Login />,
    },
    {
        path: "/register",
        element: <Register />,
    },
    {
        element: <RequireAuth />,
        children: [
            {
                path: "/",
                element: <Root />,
                children: [
                    {
                        index: true,
                        element: <Profile />,
                    },
                    {
                        path: "profile",
                        element: <Profile />,
                    },
                    {
                        path: "history",
                        element: <History />,
                    },
                    {
                        path: "workout-template",
                        element: <WorkoutTemplate />,
                    },
                    {
                        path: "exercises",
                        element: <Exercises />,
                    },
                    {
                        path: "create-workout",
                        element: <CreateWorkout />,
                    },
                    {
                        path: "create-template",
                        element: <CreateWorkoutTempalte />,
                    },
                    {
                        element: <RequireAdminRole />,
                        children: [
                            {
                                path: "create-exercise/:id?",
                                element: <CreateExercise />,
                            },
                        ],
                    },
                ],
            },
        ],
    },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <Provider store={store}>
            <RouterProvider router={router} />
            <Analytics debug={false} />
        </Provider>
    </React.StrictMode>
);
