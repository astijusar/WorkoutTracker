import BottomNav from "../components/BottomNav";
import { Outlet } from "react-router-dom";

const Root = () => {
    return (
        <>
            <div className="pb-[80px] w-full min-h-screen text-slate-300 bg-neutral flow-root sm:rounded-t-2xl sm:mx-auto sm:w-2/3 md:w-3/5 lg:w-3/6 xl:w-1/3">
                <Outlet />
            </div>
            <BottomNav />
        </>
    );
};

export default Root;
