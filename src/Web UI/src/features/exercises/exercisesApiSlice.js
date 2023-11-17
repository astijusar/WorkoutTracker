import { apiSlice } from "../api/apiSlice";

const exercisesApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getExercises: builder.query({
            query: (params) => {
                return {
                    url: "/exercise",
                    params,
                };
            },
            providesTags: (result, error, arg) => [
                ...result.data.map((exercise) => ({
                    type: "Exercise",
                    id: exercise.id,
                })),
                { type: "Exercise", id: "PARTIAL-LIST" },
            ],
        }),
    }),
});

export const { useGetExercisesQuery } = exercisesApiSlice;
