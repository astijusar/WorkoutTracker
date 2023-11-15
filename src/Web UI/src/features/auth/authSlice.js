import { createSlice } from "@reduxjs/toolkit";
import { jwtDecode } from "jwt-decode";
import Cookies from "js-cookie";

const authSlice = createSlice({
    name: "auth",
    initialState: {
        user: null,
        accessToken: Cookies.get("accessToken") ?? null,
        refreshToken: Cookies.get("refreshToken") ?? null,
        roles: null,
    },
    reducers: {
        setCredentials: (state, action) => {
            const { user, accessToken, refreshToken } = action.payload;
            state.user = user;
            state.accessToken = accessToken;
            state.refreshToken = refreshToken;
            state.roles =
                jwtDecode(accessToken)[
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                ];
            // not very secure
            Cookies.set("accessToken", accessToken, {
                expires: 10 / (24 * 60),
                secure: true,
            });
            Cookies.set("refreshToken", refreshToken, {
                expires: 1,
                secure: true,
            });
        },
        logOut: (state, action) => {
            state.user = null;
            state.accessToken = null;
            state.refreshToken = null;
            state.roles = null;
            Cookies.remove("accessToken");
            Cookies.remove("refreshToken");
        },
    },
});

export const { setCredentials, logOut } = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentUser = (state) => state.auth.user;
export const selectCurrentToken = (state) => state.auth.accessToken;
export const selectCurrentRefreshToken = (state) => state.auth.refreshToken;
export const selectCurrentUserRoles = (state) => state.auth.roles;
