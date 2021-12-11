using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public class FleeBehaviour
    {
        public static Vector2 RunAway(Animal animal)
        {
            Vector2 targetPosition = PursueBehaviour.Chase(animal);
            Vector2 desiredVelocity = Vector2.Multiply(-targetPosition + animal.Position, animal.MaxSpeed);
            return desiredVelocity;
        }
    }
}
