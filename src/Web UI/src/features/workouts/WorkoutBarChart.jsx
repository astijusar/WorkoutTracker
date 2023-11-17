import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";
import { useEffect, useRef } from "react";
import moment from "moment";

const WorkoutBarChart = ({ workouts }) => {
    const chartRef = useRef(null);

    ChartJS.register(
        CategoryScale,
        LinearScale,
        BarElement,
        Title,
        Tooltip,
        Legend
    );

    const generateWeekLabels = () => {
        const labels = [];

        for (let i = 5; i >= 0; i--) {
            const week = moment()
                .subtract(i, "weeks")
                .startOf("isoWeek")
                .format("MM/D");
            labels.push(week);
        }

        return labels;
    };

    const calculateWorkoutsPerWeek = () => {
        const counts = Array(6).fill(0);

        workouts.forEach((workout) => {
            const weekIndex = moment().diff(moment(workout.start), "weeks");
            if (weekIndex >= 0 && weekIndex < 6) {
                counts[weekIndex]++;
            }
        });

        return counts.reverse();
    };

    const options = {
        responsive: true,
        plugins: {
            title: {
                display: true,
                text: "Workouts per week",
                font: {
                    size: 20,
                },
                color: "#C8CAD0",
            },
            legend: {
                display: false,
            },
        },
        scales: {
            x: {
                ticks: {
                    color: "#9ca3af",
                },
            },
            y: {
                ticks: {
                    beginAtZero: true,
                    stepSize: 1,
                    color: "#9ca3af",
                },
                suggestedMin: 0,
                suggestedMax: 4,
            },
        },
    };

    const weekLabels = generateWeekLabels();
    const workoutCounts = calculateWorkoutsPerWeek();

    const data = {
        labels: weekLabels,
        datasets: [
            {
                data: workoutCounts,
                backgroundColor: "#606FF6",
                borderRadius: 5,
            },
        ],
    };

    useEffect(() => {
        const chartInstance = chartRef.current;

        if (chartInstance) {
            chartInstance.data = data;
            chartInstance.update();
        }
    }, [data]);

    return <Bar ref={chartRef} options={options} data={data} />;
};

export default WorkoutBarChart;
