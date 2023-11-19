import { useStopwatch } from "react-timer-hook";
import moment from "moment";

const Stopwatch = ({ start }) => {
    const offsetSeconds = start ? moment().diff(moment(start), "seconds") : 0;
    let stopwatchOffset = new Date();
    stopwatchOffset.setSeconds(stopwatchOffset.getSeconds() + offsetSeconds);

    const { seconds, minutes, hours } = useStopwatch({
        autoStart: true,
        offsetTimestamp: stopwatchOffset,
    });

    return (
        <>
            {hours < 10 ? `0${hours}` : hours}:
            {minutes < 10 ? `0${minutes}` : minutes}:
            {seconds < 10 ? `0${seconds}` : seconds}
        </>
    );
};

export default Stopwatch;
