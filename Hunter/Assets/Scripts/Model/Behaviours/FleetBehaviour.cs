using System.Numerics;
using Hunter.Model.Entities;
using System.Collections.Generic;

namespace Hunter.Model.Behaviours
{
    public class FleetBehaviour
    {
        public static Vector2 RunAway(Animal animal)
        {
            Vector2 GetFleeTargetPosition()
            {
                List<Entity> target = new List<Entity>();
                float minDistance = float.MaxValue;
                Vector2 _targetPosition = animal.Position;
                if (animal.Entities.Count != 0)
                {
                    target.Add(animal.Entities[0]);
                    foreach (Entity entity in animal.Entities)
                    {
                        float targetDistance = Vector2.Distance(animal.Position, target[0].Position);
                        if (targetDistance < minDistance)
                        {
                            minDistance = targetDistance;
                            _targetPosition = entity.Position;
                        }
                        else continue;
                    }
                }
                else
                {
                    _targetPosition = Vector2.Zero;
                }
                return _targetPosition;
            }
            Vector2 targetPosition = GetFleeTargetPosition();
            Vector2 desiredVelocity = Vector2.Multiply(-targetPosition + animal.Position, animal.MaxSpeed);
            return desiredVelocity;
        }

    }
}
