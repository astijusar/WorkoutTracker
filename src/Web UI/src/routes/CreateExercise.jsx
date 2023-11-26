import { useParams, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";

const CreateExercise = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm();

    const muscleGroups = [
        "Biceps",
        "Abdominals",
        "Triceps",
        "Back",
        "Calves",
        "Chest",
        "Forearms",
        "Glutes",
        "Hamstrings",
        "Lats",
        "LowerBack",
        "Neck",
        "Quadriceps",
        "Shoulders",
        "Traps",
    ];

    const equipmentTypes = [
        "Barbell",
        "Dumbbell",
        "Kettlebell",
        "Bodyweight",
        "Machine",
        "Cable",
        "Plate",
        "Band",
        "Other",
        "None",
    ];

    const onSubmit = (data) => {
        console.log(data);
    };

    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">
                {id === undefined ? "Create Exercise" : "Edit Exercise"}
            </h1>
            <form className="mt-10" onSubmit={handleSubmit(onSubmit)}>
                <div>
                    <label
                        className="block mb-2 text-lg font-medium"
                        htmlFor="name"
                    >
                        Exercise name:
                    </label>
                    <input
                        {...register("exerciseName", { required: true })}
                        type="text"
                        className="input w-full"
                        id="name"
                        required
                    />
                </div>
                <div className="mt-3">
                    <label
                        className="block mb-2 text-lg font-medium"
                        htmlFor="instructions"
                    >
                        Instructions:
                    </label>
                    <textarea
                        {...register("instructions")}
                        className="textarea w-full h-32"
                        id="instructions"
                    ></textarea>
                </div>
                <div className="mt-3 flex gap-x-3">
                    <div className="w-full">
                        <label
                            className="block mb-2 text-lg font-medium"
                            htmlFor="muscleGroup"
                        >
                            Muscle group:
                        </label>
                        <select
                            className="select w-full"
                            id="muscleGroup"
                            {...register("muscleGroup", { required: true })}
                        >
                            {muscleGroups.map((muscleGroup) => (
                                <option key={muscleGroup} value={muscleGroup}>
                                    {muscleGroup}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className="w-full">
                        <label
                            className="block mb-2 text-lg font-medium"
                            htmlFor="equipmentType"
                        >
                            Equipment type:
                        </label>
                        <select
                            className="select w-full"
                            id="equipmentType"
                            {...register("equipmentType", { required: true })}
                        >
                            {equipmentTypes.map((equipmentType) => (
                                <option
                                    key={equipmentType}
                                    value={equipmentType}
                                >
                                    {equipmentType}
                                </option>
                            ))}
                        </select>
                    </div>
                </div>
                <button className="btn btn-secondary mt-8 w-full tracking-wider">
                    {id === undefined ? "CREATE EXERCISE" : "UPDATE EXERCISE"}
                </button>
                <button
                    className="btn btn-error mt-4 w-full hover:bg-red-500 tracking-wider"
                    onClick={() => navigate("/exercises")}
                >
                    CANCEL
                </button>
            </form>
        </div>
    );
};

export default CreateExercise;
