import { useLocation, Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectCurrentUserRoles } from "./authSlice";

const RequireAdminRole = () => {
    const userRoles = useSelector(selectCurrentUserRoles);
    const location = useLocation();

    return userRoles.includes("Admin") ? (
        <Outlet />
    ) : (
        <Navigate to="/profile" state={{ from: location }} replace />
    );
};

export default RequireAdminRole;
