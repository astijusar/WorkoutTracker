import HistoryCard from "../components/HistoryCard";

const History = () => {
    return (
        <div className="mx-5">
            <h1 className="mt-5 text-5xl font-semibold">History</h1>
            <div className="mt-7 flex justify-between">
                <h5 className="text-sm text-gray-400 tracking-widest">NOVEMBER</h5>
                <h5 className="text-sm text-gray-400">4 workouts</h5>
            </div>
            <div className="mt-3 flex flex-col gap-4">
                <HistoryCard />
                <HistoryCard />
                <HistoryCard />
                <HistoryCard />
                <HistoryCard />
            </div>
        </div>
    );
}

export default History;