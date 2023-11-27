import { apiSlice } from "../api/apiSlice";
import moment from "moment";

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
        addNewWorkout: builder.mutation({
            query: (initialWorkout) => ({
                url: "/workout",
                method: "POST",
                body: {
                    name: initialWorkout.name
                        ? initialWorkout.name
                        : "New Workout",
                    note: initialWorkout.note,
                    start: initialWorkout.start,
                    end: moment().format(),
                    isTemplate: false,
                },
            }),
            invalidatesTags: [{ type: "Workout", id: "PARTIAL-LIST" }],
        }),
        addNewWorkoutTemplate: builder.mutation({
            query: (initialWorkout) => ({
                url: "/workout",
                method: "POST",
                body: {
                    name: initialWorkout.name
                        ? initialWorkout.name
                        : "New Template",
                    note: initialWorkout.note,
                    start: null,
                    end: null,
                    isTemplate: true,
                },
            }),
            invalidatesTags: [{ type: "Workout", id: "PARTIAL-LIST" }],
        }),
        updateWorkoutTemplate: builder.mutation({
            query: ({ templateId, updatedTemplate }) => ({
                url: `/workout/${templateId}`,
                method: "PUT",
                body: {
                    name: updatedTemplate.name
                        ? updatedTemplate.name
                        : "New Template",
                    note: updatedTemplate.note,
                    start: null,
                    end: null,
                    isTemplate: true,
                },
            }),
            invalidatesTags: [{ type: "Workout", id: "PARTIAL-LIST" }],
        }),
        addNewWorkoutExercises: builder.mutation({
            query: (request) => {
                const body = request.exercises.map((exercise) => ({
                    exerciseId: exercise.exerciseId,
                    sets: request.isTemplate
                        ? exercise.sets
                        : exercise.sets
                              .filter((set) => set.done)
                              .map((set) => ({
                                  reps: set.reps,
                                  weight: set.weight,
                                  done: set.done,
                                  measurementType: 1,
                              })),
                }));

                return {
                    url: `workout/${request.workoutId}/exercise/collection`,
                    method: "POST",
                    body: body,
                };
            },
            invalidatesTags: [{ type: "Workout", id: "PARTIAL-LIST" }],
        }),
        deleteWorkout: builder.mutation({
            query: ({ id }) => ({
                url: `workout/${id}`,
                method: "DELETE",
            }),
            invalidatesTags: (result, error, id) => [
                { type: "Workout", id },
                { type: "Workout", id: "PARTIAL-LIST" },
            ],
            onQueryStarted: async ({ id }, { dispatch, queryFulfilled }) => {
                const deleteResult = dispatch(
                    workoutApiSlice.util.updateQueryData(
                        "getWorkouts",
                        null,
                        (draft) => {
                            const index = draft.data.findIndex(
                                (workout) => workout.id === id
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
    useGetWorkoutsQuery,
    useAddNewWorkoutMutation,
    useAddNewWorkoutTemplateMutation,
    useAddNewWorkoutExercisesMutation,
    useDeleteWorkoutMutation,
    useUpdateWorkoutTemplateMutation,
} = workoutApiSlice;
