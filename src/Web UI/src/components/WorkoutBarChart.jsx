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

    for (let i = 7; i >= 0; i--) {
        const week = moment().subtract(i, "weeks").startOf('isoWeek').format("MM/D");
        labels.push(week);
    }

    return labels;
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
            color: '#C8CAD0'
        },
        legend: {
            display: false,
        },
    },
    scales: {
        x: {
            ticks: {
                color: '#9ca3af'
            }
        },
        y: {
            ticks: {
                beginAtZero: true,
                stepSize: 1,
                color: '#9ca3af'
            },
        },
    },
};

const weekLabels = generateWeekLabels();

const data = {
    labels: weekLabels,
    datasets: [
        {
            data: weekLabels.map(() => Math.floor(Math.random() * 6)),
            backgroundColor: '#606FF6'
        },
    ],
};

const WorkoutBarChart = () => {
    const chartRef = useRef(null);

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
