using System;
using System.Numerics;
using Hunter.Model.Entities;

public static class Behaviours
{
    private static readonly Random s_random = new();
    private static readonly float s_circleDistance = 10;
    private static readonly float s_circleRadius = 1;
    private static readonly float s_maxDisplacement = 1;

    public static Vector2 Wander(Animal entity)
    {
        Vector2 circleCenter = entity.Velocity;
        circleCenter = Vector2.Normalize(circleCenter);
        circleCenter *= s_circleDistance;

        Vector2 displacement = new Vector2(s_maxDisplacement,
            s_maxDisplacement);
        displacement *= s_circleRadius;

        float vectorlength = displacement.Length();

        displacement.X = GetRandomAngle(entity.WanderAngle, vectorlength);
        displacement.Y = GetRandomAngle(entity.WanderAngle, vectorlength);

        entity.WanderAngle += s_random.Next((int)-s_maxDisplacement,
            (int)s_maxDisplacement) *
            s_maxDisplacement - s_maxDisplacement * 0.5f;

        Vector2 desiredVelocity = Vector2.Add(circleCenter, displacement);

        return desiredVelocity;
    }

    private static float GetRandomAngle(float wanderAngle, float vectorlength)
    {
        int randValue = s_random.Next(0, 1);
        if (randValue == 0)
        {
            return (float)Math.Cos(wanderAngle * vectorlength);
        }

        return (float)Math.Sin(wanderAngle * vectorlength);
    }
}
