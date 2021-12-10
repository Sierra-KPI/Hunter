using System;
using System.Numerics;
using Hunter.Model.Entities;
using Hunter.Model;
using System.Linq;
using System.Collections.Generic;

namespace Hunter.Model.Behaviours
{
    public class PursueBehaviour
    {
        public static Vector2 Chase(Animal animal)
        {
            List<Entity> target = new List<Entity>(1);
            Vector2 targetPosition = target[0].Position;
            Vector2 GetTargetPosition()
            {
                
                float minDistance = float.MaxValue;
                foreach (Entity entity in animal.Entities)
                {
                    //if (animal.Entities.Length = 0)
                    //{
                    //  targetPosition = new Vector2(0, 0);
                    //}
                    float targetDistance = Vector2.Distance(animal.Position, targetPosition);
                    if (targetDistance < minDistance)
                    {
                        minDistance = targetDistance;
                        target[0] = entity;
                    }
                    else continue;
                }
                return targetPosition;
            }
            //Vector2 targetPosition = GetTarget()[0].Position;
            Vector2 desiredVelocity = Vector2.Add(animal.Position, targetPosition);
            return desiredVelocity;
        }

    }
}
