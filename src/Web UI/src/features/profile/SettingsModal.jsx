import { useDispatch } from "react-redux";
import { logOut } from "../auth/authSlice";

const SettingsModal = ({ modalRef }) => {
    const dispatch = useDispatch();

    return (
        <dialog ref={modalRef} className="modal">
            <div className="modal-box">
                <form method="dialog">
                    <button className="btn btn-sm btn-circle btn-ghost p-3 absolute right-2 top-2">
                        ✕
                    </button>
                </form>
                <div className="flex flex-col gap-4">
                    <h3 className="font-bold text-2xl">Settings</h3>
                    <button className="btn btn-warning w-full hover:bg-yellow-500">
                        DELETE WORKOUT HISTORY
                    </button>
                    <button
                        className="btn btn-error w-full hover:bg-red-500"
                        onClick={() => dispatch(logOut())}
                    >
                        LOG OUT
                    </button>
                </div>
            </div>
            <form method="dialog" className="modal-backdrop">
                <button>close</button>
            </form>
        </dialog>
    );
};

export default SettingsModal;
