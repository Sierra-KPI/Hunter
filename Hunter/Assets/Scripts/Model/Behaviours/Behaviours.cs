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

    public static Vector2 GetHerdVelocity(HerdAnimal[] herdAnimals, HerdAnimal currentAnimal)
    {
        Vector2 desiredVelocity = Vector2.Zero;
        desiredVelocity += Cohesion(herdAnimals, currentAnimal);
        desiredVelocity += Separation(herdAnimals, currentAnimal);
        desiredVelocity += Alignment(herdAnimals, currentAnimal);


        return desiredVelocity;
    }

    private static Vector2 Cohesion(HerdAnimal[] herdAnimals, HerdAnimal currentAnimal)
    {
        Vector2 perceivedCentre = Vector2.Zero;
        foreach (HerdAnimal animal in herdAnimals)
        {
            if (animal != currentAnimal) perceivedCentre += animal.Position;

        }
        perceivedCentre = perceivedCentre / (herdAnimals.GetLength(0) - 1);
        return (perceivedCentre - currentAnimal.Position) / 5;
    }

    private static Vector2 Separation(HerdAnimal[] herdAnimals, HerdAnimal currentAnimal)
    {
        Vector2 distance = Vector2.Zero;
        foreach (HerdAnimal animal in herdAnimals)
        {
            if (animal != currentAnimal)
            {
                if ((animal.Position - currentAnimal.Position).LengthSquared() < 5)
                {
                    distance = distance - (animal.Position - currentAnimal.Position);
                }
            }

        }
        return distance;
    }

    private static Vector2 Alignment(HerdAnimal[] herdAnimals, HerdAnimal currentAnimal)
    {
        Vector2 perceivedVelocity = Vector2.Zero;

        foreach (HerdAnimal animal in herdAnimals)
        {
            if (animal != currentAnimal) perceivedVelocity += animal.Velocity;

        }
        perceivedVelocity = perceivedVelocity / (herdAnimals.GetLength(0) - 1);

        return (perceivedVelocity - currentAnimal.Velocity) / 8;
    }




}
