import { useParams, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import {
    useGetExerciseByIdQuery,
    useAddNewExerciseMutation,
    useUpdateExerciseMutation,
} from "../features/exercises/exercisesApiSlice";
import { useEffect } from "react";

const CreateExercise = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const { data: exercise, isLoading: isExerciseLoading } =
        id !== undefined
            ? useGetExerciseByIdQuery(id)
            : { data: null, isLoading: false };
    const [addNewExercise, { isLoading: isAddExerciseLoading }] =
        useAddNewExerciseMutation();
    const [updateExercise, { isLoading: isUpdateExerciseLoading }] =
        useUpdateExerciseMutation();

    const canSubmit =
        !isExerciseLoading && !isAddExerciseLoading && !isUpdateExerciseLoading;

    const {
        register,
        handleSubmit,
        formState: { errors },
        setValue,
    } = useForm({
        defaultValues:
            id !== undefined
                ? {
                      name: exercise?.name,
                      instructions: exercise?.instructions,
                      muscleGroup: exercise?.muscleGroup,
                      equipmentType: exercise?.equipmentType,
                  }
                : {},
    });

    useEffect(() => {
        if (exercise) {
            Object.keys(exercise).forEach((key) => {
                setValue(key, exercise[key]);
            });
        }
    }, [exercise, setValue]);

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

    const onSubmit = async (data) => {
        if (id === undefined) {
            await addNewExercise(data);
        } else {
            await updateExercise({ exerciseId: id, updatedExercise: data });
        }

        navigate("/exercises");
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
                        {...register("name", { required: true })}
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
                <button
                    className="btn btn-secondary mt-8 w-full tracking-wider disabled:border-secondary disabled:text-slate-400"
                    disabled={!canSubmit}
                >
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
