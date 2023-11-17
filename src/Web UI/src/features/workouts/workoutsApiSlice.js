import { apiSlice } from "../api/apiSlice";

const workoutApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getWorkouts: builder.query({
            query: (params) => {
                return {
                    url: "/workout",
                    params,
                };
            },
            providesTags: (result, error, arg) =>
                result
                    ? [
                          ...result.data.map(({ id }) => ({
                              type: "Workout",
                              id,
                          })),
                          { type: "Workout", id: "PARTIAL-LIST" },
                      ]
                    : [{ type: "Workout", id: "PARTIAL-LIST" }],
        }),
    }),
});

export const { useGetWorkoutsQuery } = workoutApiSlice;
