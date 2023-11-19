const DeletionWarningModal = ({ modalRef, message, type }) => {
    return (
        <dialog ref={modalRef} className="modal">
            <div className="modal-box p-0">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <div className="flex justify-center items-center">
                    <h1 className="font-bold text-3xl">{message}</h1>
                </div>
                <div className="flex justify-center items-center">
                    <button className="btn btn-error">Delete</button>
                    <button className="btn btn-secondary">Cancel</button>
                </div>
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default DeletionWarningModal;
