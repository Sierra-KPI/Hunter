using System;
using System.Numerics;
using Hunter.Model.Entities;

public static class Behaviours
{
    private static readonly Random s_random = new();
    private static readonly float s_circleDistance = 10;
    private static readonly float s_circleRadius = 4;
    private static readonly float s_maxDisplacement = 3;

    public static Vector2 Wander(Animal entity)
    {
        Vector2 circleCenter = GetCircleCenter(entity.Velocity);

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

    private static Vector2 GetCircleCenter(Vector2 velocity)
    {
        Vector2 circleCenter = velocity;
        circleCenter = Vector2.Normalize(circleCenter);

        return circleCenter *= s_circleDistance;
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

    public static Vector2 GetHerdVelocity(Deer[] Deers, Deer currentDeer)
    {
        Vector2 desiredVelocity = Vector2.Zero;
        desiredVelocity += Cohesion(Deers, currentDeer);
        //desiredVelocity += Separation(Deers, currentDeer);
        desiredVelocity += Alignment(Deers, currentDeer);


        return desiredVelocity;
    }

    private static Vector2 Cohesion(Deer[] Deers, Deer currentDeer)
    {
        Vector2 perceivedCentre = Vector2.Zero;
        foreach (var deer in Deers)
        {
            if (deer != currentDeer) perceivedCentre += deer.Position;

        }
        perceivedCentre = perceivedCentre / (Deers.GetLength(0) - 1);
        return (perceivedCentre - currentDeer.Position) / 10;
    }

    private static Vector2 Separation(Deer[] Deers, Deer currentDeer)
    {
        Vector2 distance = Vector2.Zero;
        foreach (var deer in Deers)
        {
            if (deer != currentDeer)
            {
                if ((deer.Position - currentDeer.Position).Length() < 10)
                {
                    distance = distance - (deer.Position - currentDeer.Position);
                }
            }

        }
        return distance;
    }

    private static Vector2 Alignment(Deer[] Deers, Deer currentDeer)
    {
        Vector2 perceivedVelocity = Vector2.Zero;

        foreach (var deer in Deers)
        {
            if (deer != currentDeer) perceivedVelocity += deer.Velocity;

        }
        perceivedVelocity = perceivedVelocity / (Deers.GetLength(0) - 1);

        return (perceivedVelocity - currentDeer.Velocity) / 8;
    }




}
