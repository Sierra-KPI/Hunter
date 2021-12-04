using System;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public class HerdBehaviour
    {

        private static float _distance = 2f;
        private static float _cohesionCoef = 0.2f;
        private static float _alignmentCoef = 0.25f;

        public static Vector2 GetHerdVelocity(HerdAnimal[] herdAnimals,
            HerdAnimal currentAnimal)
        {
            Vector2 perceivedCentre = Vector2.Zero;
            Vector2 separation = Vector2.Zero;
            Vector2 perceivedVelocity = Vector2.Zero;

            foreach (HerdAnimal animal in herdAnimals)
            {
                if (animal != currentAnimal)
                {
                    perceivedCentre += animal.Position;

                    if ((animal.Position -
                        currentAnimal.Position).LengthSquared() < _distance)
                    {
                        separation -= (animal.Position -
                            currentAnimal.Position);
                    }

                    perceivedVelocity += animal.Velocity;
                }
            }

            perceivedCentre /= herdAnimals.GetLength(0) - 1;
            Vector2 cohesion = (perceivedCentre -
                currentAnimal.Position) * _cohesionCoef;

            perceivedVelocity /= (herdAnimals.GetLength(0) - 1);
            Vector2 alignment = (perceivedVelocity -
                currentAnimal.Velocity) * _alignmentCoef;

            return cohesion + separation + alignment;
        }
    }
}
