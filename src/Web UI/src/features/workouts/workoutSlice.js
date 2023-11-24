import { createSlice, createSelector } from "@reduxjs/toolkit";

const initialState = {
    name: "New workout",
    note: "",
    start: null,
    end: null,
    isTemplate: false,
    exercises: [],
};

const workoutSlice = createSlice({
    name: "workout",
    initialState,
    reducers: {
        updateWorkout(state, action) {
            const { name, note, start, end, isTemplate, exercises } =
                action.payload;
            state.name = name;
            state.note = note;
            state.start = start;
            state.end = end;
            state.isTemplate = isTemplate;
            state.exercises = exercises;
        },
        addExercise(state, action) {
            const { id, exerciseId, name } = action.payload;
            state.exercises.push({
                id: id,
                exerciseId: exerciseId,
                name: name,
                sets: [{ reps: 0, weight: 0, done: false }],
                errors: false,
            });
        },
        updateExercise(state, action) {
            const exercise = action.payload;
            const existingExerciseIndex = state.exercises.findIndex(
                (ex) => ex.id === exercise.id
            );
            if (existingExerciseIndex !== -1) {
                state.exercises[existingExerciseIndex] = exercise;
            }
        },
        removeExercise(state, action) {
            const { id } = action.payload;
            const existingExerciseIndex = state.exercises.findIndex(
                (ex) => ex.id === id
            );
            if (existingExerciseIndex !== -1) {
                state.exercises.splice(existingExerciseIndex, 1);
            }
        },
    },
});

export const { updateWorkout, addExercise, updateExercise, removeExercise } =
    workoutSlice.actions;

export default workoutSlice.reducer;

export const selectWorkout = (state) => state.workout;
export const selectWorkoutExercises = (state) => state.workout.exercises;
export const selectWorkoutExerciseCount = (state) =>
    state.workout.exercises.length;
