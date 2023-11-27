import { useForm } from "react-hook-form";
import { useUpdateWorkoutTemplateMutation } from "./workoutsApiSlice";
import { useNavigate } from "react-router-dom";

const WorkoutTemplateEditModal = ({ template, modalRef }) => {
    const [updateTemplate, { isLoading }] = useUpdateWorkoutTemplateMutation();
    const navigate = useNavigate();

    const { register, handleSubmit } = useForm({
        defaultValues: {
            name: template.name,
            note: template.note,
        },
    });

    const onSubmit = async (data) => {
        await updateTemplate({
            templateId: template.id,
            updatedTemplate: data,
        });
        navigate("/workout-template");
    };

    return (
        <dialog ref={modalRef} className="modal" id="addExerciseModal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <div className="p-3 flex flex-col">
                    <h1 className="font-semibold text-3xl">Edit template</h1>
                    <form className="mt-5" onSubmit={handleSubmit(onSubmit)}>
                        <div className="mb-3">
                            <label
                                className="block mb-2 text-lg font-medium"
                                htmlFor="name"
                            >
                                Name:
                            </label>
                            <input
                                {...register("name", { required: true })}
                                type="text"
                                className="input w-full bg-neutral"
                                id="name"
                                required
                            />
                        </div>
                        <div>
                            <label
                                className="block mb-2 text-lg font-medium"
                                htmlFor="note"
                            >
                                Note:
                            </label>
                            <input
                                {...register("note")}
                                type="text"
                                className="input w-full bg-neutral"
                                id="note"
                            />
                        </div>
                        <button
                            className="btn btn-secondary mt-8 w-full tracking-wider disabled:border-secondary disabled:text-slate-400"
                            disabled={isLoading}
                        >
                            UPDATE TEMPLATE
                        </button>
                        <button
                            className="btn btn-error mt-4 w-full hover:bg-red-500 tracking-wider"
                            onClick={() => navigate("/workout-template")}
                        >
                            CANCEL
                        </button>
                    </form>
                </div>
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default WorkoutTemplateEditModal;
