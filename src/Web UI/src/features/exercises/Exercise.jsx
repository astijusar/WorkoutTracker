const Exercise = ({ exercise }) => {
    return (
        <div key={exercise.id} className="flex items-center gap-3">
            <div className="w-16 h-16 bg-secondary rounded-full flex items-center justify-center text-black font-semibold">
                {exercise.name[0]}
            </div>
            <div className="flex flex-col">
                <h5 className="font-semibold text-xl">{exercise.name}</h5>
                <p className="text-gray-500 text-sm">{exercise.muscleGroup}</p>
            </div>
        </div>
    );
};

export default Exercise;
