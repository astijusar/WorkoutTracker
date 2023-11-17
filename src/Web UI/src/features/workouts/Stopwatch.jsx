import { useStopwatch } from "react-timer-hook";

const Stopwatch = () => {
    const { seconds, minutes, hours } = useStopwatch({ autoStart: true });

    return (
        <>
            {hours < 10 ? `0${hours}` : hours}:
            {minutes < 10 ? `0${minutes}` : minutes}:
            {seconds < 10 ? `0${seconds}` : seconds}
        </>
    );
};

export default Stopwatch;
