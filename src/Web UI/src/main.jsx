import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./index.css";
import Root from "./routes/root.jsx";
import Profile from "./routes/Profile.jsx";
import StartWorkout from "./routes/StartWorkout.jsx";
import Exercises from "./routes/Exercises.jsx";
import History from "./routes/History.jsx";

import { store } from "./app/store.js";
import { Provider } from "react-redux";
import Login from "./routes/Login.jsx";
import RequireAuth from "./auth/RequireAuth.jsx";
import Register from "./routes/Register.jsx";

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
                        path: "start-workout",
                        element: <StartWorkout />,
                    },
                    {
                        path: "exercises",
                        element: <Exercises />,
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
        </Provider>
    </React.StrictMode>
);
