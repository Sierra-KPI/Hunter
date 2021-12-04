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
    }
}
