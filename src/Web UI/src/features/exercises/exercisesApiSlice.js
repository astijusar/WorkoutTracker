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
            providesTags: (result, error, arg) =>
                result
                    ? [
                          ...result.data.map((exercise) => ({
                              type: "Exercise",
                              id: exercise.id,
                          })),
                          { type: "Exercise", id: "PARTIAL-LIST" },
                      ]
                    : [{ type: "Exercise", id: "PARTIAL-LIST" }],
        }),
        getExerciseById: builder.query({
            query: (exerciseId) => {
                return {
                    url: `/exercise/${exerciseId}`,
                };
            },
            providesTags: (result, error, id) => [
                { type: "Exercise", id: id },
                { type: "Exercise", id: "PARTIAL-LIST" },
            ],
        }),
        addNewExercise: builder.mutation({
            query: (exercise) => ({
                url: "/exercise",
                method: "POST",
                body: exercise,
            }),
            invalidatesTags: [{ type: "Exercise", id: "PARTIAL-LIST" }],
        }),
        updateExercise: builder.mutation({
            query: ({ exerciseId, updatedExercise }) => ({
                url: `/exercise/${exerciseId}`,
                method: "PUT",
                body: updatedExercise,
            }),
            invalidatesTags: (result, error, id) => [
                { type: "Exercise", id: id },
                { type: "Exercise", id: "PARTIAL-LIST" },
            ],
        }),
        deleteExercise: builder.mutation({
            query: (exerciseId) => ({
                url: `/exercise/${exerciseId}`,
                method: "DELETE",
            }),
            invalidatesTags: (result, error, id) => [
                { type: "Exercise", id: id },
            ],
            onQueryStarted: async (id, { dispatch, queryFulfilled }) => {
                const deleteResult = dispatch(
                    exercisesApiSlice.util.updateQueryData(
                        "getExercises",
                        null,
                        (draft) => {
                            const index = draft.data.findIndex(
                                (exercise) => exercise.id === id
                            );
                            if (index !== -1) {
                                draft.data.splice(index, 1);
                            }
                        }
                    )
                );

                try {
                    await queryFulfilled;
                } catch {
                    deleteResult.undo();
                }
            },
        }),
    }),
});

export const {
    useGetExercisesQuery,
    useGetExerciseByIdQuery,
    useAddNewExerciseMutation,
    useUpdateExerciseMutation,
    useDeleteExerciseMutation,
} = exercisesApiSlice;
