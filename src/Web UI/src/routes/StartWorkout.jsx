import SampleTemplateCard from "../components/SampleTemplateCard";
import TemplateCard from "../components/TemplateCard";

const StartWorkout = () => {
    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">Workout</h1>
            <h5 className="mt-5 text-sm text-gray-400 tracking-widest">QUICK START</h5>
            <button className="mt-3 w-full btn btn-secondary tracking-widest text-white">START AN EMPTY WORKOUT</button>
            <div className="mt-3 flex justify-between">
                <h5 className="text-sm text-gray-400 tracking-widest">MY TEMPLATES</h5>
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-6 h-6"><path fillRule="evenodd" d="M12 5.25a.75.75 0 01.75.75v5.25H18a.75.75 0 010 1.5h-5.25V18a.75.75 0 01-1.5 0v-5.25H6a.75.75 0 010-1.5h5.25V6a.75.75 0 01.75-.75z" clipRule="evenodd" /></svg>
            </div>
            <div className="mt-3 flex flex-col gap-4">
                <TemplateCard />
                <TemplateCard />
                <TemplateCard />
            </div>
            <h5 className="mt-5 text-sm text-gray-400 tracking-widest">SAMPLE TEMPLATES</h5>
            <div className="mt-3 flex flex-col gap-4">
                <SampleTemplateCard />
                <SampleTemplateCard />
                <SampleTemplateCard />
            </div>
        </div>
    );
}

export default StartWorkout;