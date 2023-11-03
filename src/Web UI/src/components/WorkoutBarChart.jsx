import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
  } from 'chart.js';
import { Bar } from "react-chartjs-2";
import { useEffect, useRef } from 'react';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

const options = {
    responsive: true,
    plugins: {
        title: {
            display: true,
            text: "Workouts per week",
            font: {
                size: 20,
            }
        },
        legend: {
            display: false,
        }
    },
};

const labels = ["1", "2", "3", "4", "5", "6", "7"];

const data = {
    labels,
    datasets: [
        {
            data: labels.map(() => Math.floor(Math.random() * 4)),
        }
    ],
}

const WorkoutBarChart = () => {
    const chartRef = useRef(null);

    useEffect(() => {
        const chartInstance = chartRef.current;

        if (chartInstance) {
            chartInstance.data = data;
            chartInstance.update();
        }
    }, [data]);

    return (
        <Bar ref={chartRef} options={options} data={data} />
    )
}

export default WorkoutBarChart;