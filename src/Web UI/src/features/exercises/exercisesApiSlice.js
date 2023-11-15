import { createEntityAdapter } from "@reduxjs/toolkit";
import { apiSlice } from "../../app/api/apiSlice";

const exercisesAdapter = createEntityAdapter();
const initialState = exercisesAdapter.getInitialState();

const exercisesApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getExercises: builder.query({
            query: (param) => {
                const {
                    searchTerm,
                    muscleGroup,
                    equipmentType,
                    pageNumber,
                    pageSize,
                    sortDescending,
                } = param;
                return {
                    url: "/exercise",
                    params: {
                        searchTerm,
                        muscleGroup,
                        equipmentType,
                        pageNumber,
                        pageSize,
                        sortDescending,
                    },
                };
            },
            transformResponse: (responseData) => {
                const exercises = responseData.data;
                exercisesAdapter.setAll(initialState, exercises);
                return responseData;
            },
        }),
    }),
});

export const { useGetExercisesQuery } = exercisesApiSlice;
