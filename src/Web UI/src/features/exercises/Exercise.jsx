import { motion } from "framer-motion";
import { useRef } from "react";
import {
    addExercise,
    selectWorkoutExerciseCount,
} from "../workouts/workoutSlice";
import { useDispatch, useSelector } from "react-redux";
import ExerciseInfoModal from "./ExerciseInfoModal";

const Exercise = ({ exercise, addButton = false, listModalRef = null }) => {
    const modalRef = useRef(null);
    const dispatch = useDispatch();

    const workoutExerciseCount = useSelector(selectWorkoutExerciseCount);

    const onAddClick = () => {
        dispatch(
            addExercise({
                id: workoutExerciseCount + 1,
                exerciseId: exercise.id,
                name: exercise.name,
            })
        );

        listModalRef.current.close();
    };

    return (
        <>
            <motion.div
                key={exercise.id}
                whileHover={{ scale: 1.1 }}
                transition={{ type: "spring", stiffness: 600, damping: 20 }}
                className="flex me-5 hover:cursor-pointer"
            >
                <div
                    className="flex items-center p-2 ms-2 gap-3"
                    onClick={() => modalRef.current.showModal()}
                >
                    <div className="avatar">
                        <div className="w-16 h-16 rounded-full flex items-center justify-center">
                            <img src="/img/placeholder.jpg" />
                        </div>
                    </div>
                    <div className="flex flex-col">
                        <h5 className="font-semibold text-xl">
                            {exercise.name}
                        </h5>
                        <p className="text-gray-500 text-sm">
                            {exercise.muscleGroup}
                        </p>
                    </div>
                </div>
                {addButton && listModalRef && (
                    <div className="flex flex-grow justify-end">
                        <button
                            className="btn btn-ghost text-accent font-semibold tracking-wider"
                            onClick={() => onAddClick()}
                        >
                            ADD
                        </button>
                    </div>
                )}
            </motion.div>
            <ExerciseInfoModal exercise={exercise} modalRef={modalRef} />
        </>
    );
};

export default Exercise;
