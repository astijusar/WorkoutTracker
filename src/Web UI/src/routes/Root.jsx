import BottomNav from "../components/BottomNav";
import { Outlet } from "react-router-dom";

const Root = () => {
    return (
        <>
            <div className="w-full h-screen bg-neutral flow-root sm:rounded-t-2xl md:mx-auto md:w-4/5 lg:w-3/5 xl:w-1/2">
                <Outlet />
            </div>
            <BottomNav />
        </>
    );
}

export default Root;