import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './index.css';
import Root from './routes/root.jsx';
import Profile from './routes/Profile.jsx';
import Workout from './routes/Workout.jsx';
import Exercises from './routes/Exercises.jsx';
import History from './routes/History.jsx';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    children: [
      {
        index: true,
        element: <Profile />
      },
      {
        path: "profile",
        element: <Profile />
      },
      {
        path: "history",
        element: <History />
      },
      {
        path: "workout",
        element: <Workout />
      },
      {
        path: "exercises",
        element: <Exercises />
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
)
