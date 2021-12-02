using System;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public static class WanderBehaviour
    {
        private static readonly Random s_random = new();

        public static Vector2 Wander(Animal entity)
        {
            Vector2 circleCenter = GetCircleCenter(entity.Velocity, entity);

            Vector2 displacement = new Vector2(entity.MaxWanderShift,
                entity.MaxWanderShift);
            displacement *= entity.WanderCircleRadius;

            float vectorlength = displacement.Length();

            displacement.X = GetRandomAngle(entity.WanderAngle, vectorlength);
            displacement.Y = GetRandomAngle(entity.WanderAngle, vectorlength);

            entity.WanderAngle += s_random.Next((int)-entity.MaxWanderShift,
                (int)entity.MaxWanderShift) *
                entity.MaxWanderShift - entity.MaxWanderShift * 0.5f;

            Vector2 desiredVelocity = Vector2.Add(circleCenter, displacement);

            return desiredVelocity;
        }

        private static Vector2 GetCircleCenter(Vector2 velocity,
            Animal entity)
        {
            Vector2 circleCenter = velocity;
            circleCenter = Vector2.Normalize(circleCenter);

            return circleCenter *= entity.WanderCircleDistance;
        }

        private static float GetRandomAngle(float wanderAngle,
            float vectorlength)
        {
            int randValue = s_random.Next(0, 1);

            if (randValue == 0)
            {
                return (float)Math.Cos(wanderAngle * vectorlength);
            }

            return (float)Math.Sin(wanderAngle * vectorlength);
        }

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
