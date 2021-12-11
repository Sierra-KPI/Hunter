using System.Numerics;
using Hunter.Model.Entities;
using System.Collections.Generic;

namespace Hunter.Model.Behaviours
{
    public class PursueBehaviour
    {
        public static Vector2 Chase(Animal animal)
        {
            Vector2 GetTargetPosition()
            {
                List<Entity> target = new List<Entity>();
                float minDistance = float.MaxValue;
                Vector2 _targetPosition = animal.Position;

                if (animal.Entities.Count != 0)
                {
                    target.Add(animal.Entities[0]);

                    foreach (Entity entity in animal.Entities)
                    {
                        target[0] = entity;

                        float targetDistance = Vector2.Distance(animal.Position,
                            target[0].Position);

                        if (targetDistance < minDistance &&
                            entity.EntityType != animal.EntityType)
                        {
                            minDistance = targetDistance;
                            _targetPosition = entity.Position;
                        }
                    }
                }
                else
                {
                    _targetPosition = Vector2.Zero;
                }
                return _targetPosition;
            }
            Vector2 targetPosition = GetTargetPosition();
            Vector2 desiredVelocity = Vector2.Add(targetPosition, -animal.Position);
            return desiredVelocity;
        }

    }
}
