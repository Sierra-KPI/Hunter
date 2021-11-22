using System;
using System.Numerics;
using Hunter.Model.Entities;

public static class Behaviours
{
    public static Vector2 Wander(Animal entity)
    {
        Random random = new Random();

        Vector2 circleCenter = entity.Velocity;
        circleCenter = Vector2.Normalize(circleCenter);
        float circleDistance = 10;
        circleCenter *= circleDistance;

        float circleRadius = 1;
        Vector2 displacement = new Vector2(0, -1);
        displacement *= circleRadius;

        float vectorlength = displacement.Length();
        displacement.X = (float)Math.Cos(entity.WanderAngle * vectorlength);
        displacement.Y = (float)Math.Sin(entity.WanderAngle * vectorlength);

        float angleChange = 1;
        entity.WanderAngle += random.Next(-3, 3) *
            angleChange - angleChange * 0.5f;

        Vector2 wanderForce = Vector2.Add(circleCenter, displacement);

        return wanderForce;
    }
}
