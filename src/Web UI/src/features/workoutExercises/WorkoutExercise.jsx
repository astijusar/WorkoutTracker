const WorkoutExercise = ({ name }) => {
    return (
        <div>
            <div className="flex justify-between mb-2">
                <h5 className="font-semibold text-secondary">{name}</h5>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                    className="w-5 h-5 text-secondary me-4"
                >
                    <path
                        fillRule="evenodd"
                        d="M8.75 1A2.75 2.75 0 006 3.75v.443c-.795.077-1.584.176-2.365.298a.75.75 0 10.23 1.482l.149-.022.841 10.518A2.75 2.75 0 007.596 19h4.807a2.75 2.75 0 002.742-2.53l.841-10.52.149.023a.75.75 0 00.23-1.482A41.03 41.03 0 0014 4.193V3.75A2.75 2.75 0 0011.25 1h-2.5zM10 4c.84 0 1.673.025 2.5.075V3.75c0-.69-.56-1.25-1.25-1.25h-2.5c-.69 0-1.25.56-1.25 1.25v.325C8.327 4.025 9.16 4 10 4zM8.58 7.72a.75.75 0 00-1.5.06l.3 7.5a.75.75 0 101.5-.06l-.3-7.5zm4.34.06a.75.75 0 10-1.5-.06l-.3 7.5a.75.75 0 101.5.06l.3-7.5z"
                        clipRule="evenodd"
                    />
                </svg>
            </div>
            <table className="table">
                <thead>
                    <tr className="font-medium tracking-widest text-slate-200">
                        <th>SET</th>
                        <th>PREVIOUS</th>
                        <th>KG</th>
                        <th>REPS</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td className="font-semibold text-lg">1</td>
                        <td>30 kg x 12</td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={30}
                            />
                        </td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={12}
                            />
                        </td>
                        <td className="w-0">
                            <input
                                type="checkbox"
                                checked="checked"
                                className="checkbox checkbox-success"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td className="font-semibold text-lg">2</td>
                        <td>30 kg x 12</td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={30}
                            />
                        </td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={12}
                            />
                        </td>
                        <td className="w-0">
                            <input
                                type="checkbox"
                                checked="checked"
                                className="checkbox checkbox-success"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td className="font-semibold text-lg">3</td>
                        <td>30 kg x 10</td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={30}
                            />
                        </td>
                        <td className="font-semibold text-lg ps-0">
                            <input
                                type="text"
                                className="input w-16 h-8 text-center"
                                value={10}
                            />
                        </td>
                        <td className="w-0">
                            <input
                                type="checkbox"
                                checked={false}
                                className="checkbox checkbox-success"
                            />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div className="flex justify-center">
                <button className="btn btn-ghost font-medium tracking-widest text-success">
                    ADD SET
                </button>
                <button className="btn btn-ghost font-medium tracking-widest text-error">
                    REMOVE SET
                </button>
            </div>
        </div>
    );
};

export default WorkoutExercise;
