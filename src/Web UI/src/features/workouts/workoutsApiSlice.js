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
        addNewWorkoutExercises: builder.mutation({
            query: (request) => {
                const body = request.exercises.map((exercise) => ({
                    exerciseId: exercise.exerciseId,
                    sets: exercise.sets
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
    }),
});

export const {
    useGetWorkoutsQuery,
    useAddNewWorkoutMutation,
    useAddNewWorkoutExercisesMutation,
} = workoutApiSlice;
