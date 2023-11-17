import { motion } from "framer-motion";
import { useRef } from "react";
import ExerciseInfoModal from "./ExerciseInfoModal";

const Exercise = ({ exercise }) => {
    const modalRef = useRef(null);

    return (
        <>
            <motion.div
                key={exercise.id}
                whileHover={{ scale: 1.1 }}
                className="flex items-center p-2 gap-3 hover:cursor-pointer"
                onClick={() => modalRef.current.showModal()}
            >
                <div className="avatar">
                    <div className="w-16 h-16 rounded-full flex items-center justify-center">
                        <img src="/img/placeholder.jpg" />
                    </div>
                </div>
                <div className="flex flex-col">
                    <h5 className="font-semibold text-xl">{exercise.name}</h5>
                    <p className="text-gray-500 text-sm">
                        {exercise.muscleGroup}
                    </p>
                </div>
            </motion.div>
            <ExerciseInfoModal exercise={exercise} modalRef={modalRef} />
        </>
    );
};

export default Exercise;
