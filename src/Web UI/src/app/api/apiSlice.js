import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { setCredentials, logOut } from "../../auth/authSlice";

const baseQuery = fetchBaseQuery({
    baseUrl: "http://localhost:5264/api",
    prepareHeaders: (headers, { getState }) => {
        const token = getState().auth.accessToken;

        if (token) {
            headers.set("authorization", `Bearer ${token}`);
        }

        headers.set("Content-type", "application/json");

        return headers;
    },
});

const baseQueryWithReauth = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions);

    if (result?.error?.originalStatus === 401) {
        const refreshResult = await baseQuery(
            "/auth/refresh",
            api,
            extraOptions
        );

        if (refreshResult?.data) {
            const user = api.getState().auth.user;
            api.dispatch(setCredentials({ ...refreshResult.data, user }));
            result = await baseQuery(args, api, extraOptions);
        } else {
            api.dispatch(logOut());
        }
    }

    return result;
};

export const apiSlice = createApi({
    baseQuery: baseQueryWithReauth,
    endpoints: (builder) => ({}),
});
