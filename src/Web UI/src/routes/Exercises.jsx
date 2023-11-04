const Exercises = () => {
    const exercises = [
        "Ab Roller",
        "Bench Press (barbell)",
        "Bench Press (dumbbell)",
        "Deadlift (barbell)",
        "Deadlift (dumbbell)",
        "Jump Squat",
        "Lat Pulldown (Cable)",
        "Overhead press",
        "Ab Roller",
        "Bench Press (barbell)",
        "Bench Press (dumbbell)",
        "Deadlift (barbell)",
        "Deadlift (dumbbell)",
        "Jump Squat",
        "Lat Pulldown (Cable)",
        "Overhead press",
        "Ab Roller",
        "Bench Press (barbell)",
        "Bench Press (dumbbell)",
        "Deadlift (barbell)",
        "Deadlift (dumbbell)",
        "Jump Squat",
        "Lat Pulldown (Cable)",
        "Overhead press",
    ]
    
    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Exercises</h1>
            <div className="mt-5 flex flex-col gap-3">
                {exercises.map((ex) => 
                    <div key={ex} className="flex items-center gap-3">
                        <div className="w-16 h-16 bg-secondary rounded-full flex items-center justify-center text-black font-semibold">{ex[0]}</div>
                        <div className="flex flex-col">
                            <h5 className="font-semibold text-xl">{ex}</h5>
                            <p className="text-gray-500 text-sm">Category</p>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

export default Exercises;