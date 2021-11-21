using System.Numerics;

public static class Behaviours
{
    public static Vector2 Wander(Vector2 position, Vector2 velocity)
    {
        float wanderTheta = (float)(Math.PI / 2);
        Vector2 wanderVector = velocity;
        wanderVector = Vector2.Normalize(wanderVector) * 100;
        wanderVector.Add(position);

        int wanderRadius = 50;

        int theta = wanderTheta + velocity
    }
}
