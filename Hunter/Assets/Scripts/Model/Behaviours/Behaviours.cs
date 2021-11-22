using System;
using System.Numerics;
using Hunter.Model.Entities;

public static class Behaviours
{
    private static readonly Random s_random = new();
    private static readonly float s_circleDistance = 10;
    private static readonly float s_circleRadius = 1;
    private static readonly float s_maximumDisplacement = 1;

    public static Vector2 Wander(Animal entity)
    {
        Vector2 circleCenter = entity.Velocity;
        circleCenter = Vector2.Normalize(circleCenter);
        circleCenter *= s_circleDistance;

        Vector2 displacement = new Vector2(s_maximumDisplacement,
            s_maximumDisplacement);
        displacement *= s_circleRadius;

        float vectorlength = displacement.Length();
        displacement.X = (float)Math.Cos(entity.WanderAngle * vectorlength);
        displacement.Y = (float)Math.Sin(entity.WanderAngle * vectorlength);

        entity.WanderAngle += s_random.Next((int)-s_maximumDisplacement,
            (int)s_maximumDisplacement) *
            s_maximumDisplacement - s_maximumDisplacement * 0.5f;

        Vector2 desiredVelocity = Vector2.Add(circleCenter, displacement);

        return desiredVelocity;
    }
}
