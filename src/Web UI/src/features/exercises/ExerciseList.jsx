import Exercise from "./Exercise";

const ExerciseList = ({ exercises }) => {
    return (
        <div className="mt-5 flex flex-col gap-3">
            {exercises.map((ex) => (
                <Exercise key={ex.id} exercise={ex} />
            ))}
        </div>
    );
};

export default ExerciseList;
