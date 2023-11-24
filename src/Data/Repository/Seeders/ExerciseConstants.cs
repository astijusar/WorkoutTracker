using Core.Models;
using Core.Models.Enums;

namespace Data.Repository.Seeders
{
    internal static class ExerciseConstants
    {
        public static Guid GetExerciseId(int index) => Exercises.ElementAt(index).Id;

        public static readonly List<Exercise> Exercises = new()
        {
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Bench press",
                MuscleGroup = MuscleGroup.Chest,
                EquipmentType = Equipment.Barbell,
                Instructions = @"### Bench Press

1. **Setup:** Lie on a flat bench with your feet firmly on the ground. Grip the barbell slightly wider than shoulder-width apart.
2. **Lowering Phase:** Lower the barbell to your chest in a controlled manner, keeping your elbows at a 90-degree angle.
3. **Pressing Phase:** Push the barbell back up to the starting position, fully extending your arms.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Ensure proper form and use a spotter, especially when lifting heavy weights.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Shoulder press",
                MuscleGroup = MuscleGroup.Shoulders,
                EquipmentType = Equipment.Barbell,
                Instructions = @"### Shoulder Press

1. **Setup:** Sit or stand with a barbell on your shoulders, hands slightly wider than shoulder-width apart.
2. **Pressing Phase:** Push the barbell overhead, fully extending your arms.
3. **Lowering Phase:** Lower the barbell back to shoulder height in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Maintain a straight back and engage your core throughout the exercise.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Shoulder press",
                MuscleGroup = MuscleGroup.Shoulders,
                EquipmentType = Equipment.Dumbbell,
                Instructions = @"### Dumbbell Shoulder Press

1. **Setup:** Sit or stand with a dumbbell in each hand, elbows bent at a 90-degree angle.
2. **Pressing Phase:** Push the dumbbells overhead, fully extending your arms.
3. **Lowering Phase:** Lower the dumbbells back to shoulder height in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Maintain a neutral grip on the dumbbells and control the movement throughout.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Triceps pushdown",
                MuscleGroup = MuscleGroup.Triceps,
                EquipmentType = Equipment.Cable,
                Instructions = @"### Triceps Pushdown

1. **Setup:** Stand facing a cable machine with a straight bar attached at chest height.
2. **Extension Phase:** Push the bar downward, fully extending your arms.
3. **Flexion Phase:** Bend your elbows to bring the bar back to the starting position.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Keep your elbows close to your body and focus on contracting your triceps.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Bicep curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Barbell,
                Instructions = @"### Barbell Bicep Curl

1. **Setup:** Stand with a barbell in your hands, palms facing forward, and hands shoulder-width apart.
2. **Curling Phase:** Lift the barbell toward your chest, contracting your biceps.
3. **Extension Phase:** Lower the barbell back to the starting position in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Keep your elbows close to your body and avoid using momentum.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Dumbbell Bicep Curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Dumbbell,
                Instructions = @"### Dumbbell Bicep Curl

1. **Setup:** Stand with a dumbbell in each hand, palms facing forward, and arms fully extended.
2. **Curling Phase:** Lift the dumbbells toward your shoulders, contracting your biceps.
3. **Extension Phase:** Lower the dumbbells back to the starting position in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Keep your wrists straight and control the movement throughout.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Cable Bicep Curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Cable,
                Instructions = @"### Cable Bicep Curl

1. **Setup:** Stand facing a cable machine with a straight bar attached at the lowest setting.
2. **Curling Phase:** Pull the bar toward your shoulders, contracting your biceps.
3. **Extension Phase:** Control the bar back to the starting position.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Maintain good posture and control the cable throughout the exercise.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Lat Pulldown",
                MuscleGroup = MuscleGroup.Lats,
                EquipmentType = Equipment.Cable,
                Instructions = @"### Lat Pulldown

1. **Setup:** Sit facing a cable machine with a wide-grip bar attached overhead.
2. **Pulling Phase:** Pull the bar down to your chest, engaging your lat muscles.
3. **Extension Phase:** Release the bar back to the starting position in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Keep your chest up, and avoid using momentum to pull the bar.*"
            },
            new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Crunches",
                MuscleGroup = MuscleGroup.Abdominals,
                EquipmentType = Equipment.Bodyweight,
                Instructions = @"### Crunches

1. **Setup:** Lie on your back with your knees bent and feet flat on the ground.
2. **Crunching Phase:** Lift your upper body towards your knees, engaging your abdominal muscles.
3. **Extension Phase:** Lower your upper body back to the starting position in a controlled manner.
4. **Repeat:** Perform the desired number of repetitions.

*Note: Keep your neck in a neutral position and avoid pulling on your neck with your hands.*"
            },
        };
    }
}
