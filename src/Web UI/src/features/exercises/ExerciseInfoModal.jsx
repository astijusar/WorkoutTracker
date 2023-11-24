import Markdown from "react-markdown";

const ExerciseInfoModal = ({ exercise, modalRef }) => {
    return (
        <dialog ref={modalRef} className="modal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <h3 className="font-bold text-2xl p-3">{exercise.name}</h3>
                <div className="flex justify-center items-center w-full bg-primary h-60">
                    <h1 className="font-bold text-3xl tracking-widest">
                        EXERCISE IMAGE
                    </h1>
                </div>
                <h3 className="font-bold text-xl p-3">Instructions</h3>
                {exercise.instructions ? (
                    <Markdown className="px-5 mb-3 space-y-3">
                        {exercise.instructions}
                    </Markdown>
                ) : (
                    <p className="ps-3 mb-3 font-thin">
                        There are no instructions.
                    </p>
                )}
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default ExerciseInfoModal;
