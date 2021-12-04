using System;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public static class HerdBehaviour
    {

        public static Vector2 GetHerdVelocity(HerdAnimal[] herdAnimals,
            HerdAnimal currentAnimal)
        {
            float distance = 2f;
            float cohesionIndex = 0.2f;
            float alignmentIndex = 0.25f;

            Vector2 desiredVelocity = Vector2.Zero;

            Vector2 cohesion = Vector2.Zero;
            Vector2 perceivedCentre = Vector2.Zero;

            Vector2 separation = Vector2.Zero;

            Vector2 alignment = Vector2.Zero;
            Vector2 perceivedVelocity = Vector2.Zero;

            foreach (HerdAnimal animal in herdAnimals)
            {
                if (animal != currentAnimal)
                {
                    perceivedCentre += animal.Position;

                    if ((animal.Position -
                        currentAnimal.Position).LengthSquared() < distance)
                    {
                        separation -= (animal.Position -
                            currentAnimal.Position);
                    }

                    perceivedVelocity += animal.Velocity;
                }
            }

            perceivedCentre /= herdAnimals.GetLength(0) - 1;
            cohesion = (perceivedCentre -
                currentAnimal.Position) * cohesionIndex;

            perceivedVelocity /= (herdAnimals.GetLength(0) - 1);
            alignment = (perceivedVelocity -
                currentAnimal.Velocity) * alignmentIndex;

            return desiredVelocity + cohesion + separation + alignment;
        }
    }
}
