const BestSet = ({ exercise }) => {
    let bestSet = null;
    let highestWeight = 0;

    for (const set of exercise.sets) {
        if (set.weight > highestWeight) {
            highestWeight = set.weight;
            bestSet = set;
        }
    }

    const getMeasurementType = () => {
        switch (exercise.sets[0].measurementType) {
            case "Kilograms":
                return "kg";
            case "Grams":
                return "g";
            case "Ounces":
                return "oz";
            case "Pounds":
                return "lbs";
            case "None":
                return "";
        }
    };

    return `${bestSet.weight} ${getMeasurementType()} x ${bestSet.reps}`;
};

export default BestSet;
