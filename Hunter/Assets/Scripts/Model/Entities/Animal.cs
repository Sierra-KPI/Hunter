using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public abstract class Animal : Entity
    {
        public float BoardSeekRadius { get; set; }
        public float BodySeekRadius { get; set; }
        public List<Entity> Entities = new();
        public float WanderCircleDistance { get; set; }
        public float WanderCircleRadius { get; set; }
        public float WanderAngle { get; set; }
        public float MaxWanderShift { get; set; }

        public AnimalType AnimalType;

        public Animal()
        {
            Random random = new Random();

            float max = 1;
            float min = -1;

            float xPos = (float)(random.NextDouble() * (max - min) + min);
            float yPos = (float)(random.NextDouble() * (max - min) + min);

            if (xPos == 0)
            {
                xPos += 0.1f;
            }
            if (yPos == 0)
            {
                yPos += 0.1f;
            }

            WanderAngle = random.Next(-1, 2);
            Velocity = new Vector2(xPos, yPos);
        }

        public void GetEntitiesInArea(List<Entity> allEntities)
        {
            List<Entity> areaEntities = new List<Entity>();

            foreach (Entity entity in allEntities)
            {
                if (CollisionDetection.AreColliding(this, entity,
                    BodySeekRadius, entity.BodyRadius) && this != entity)
                {
                    areaEntities.Add(entity);
                }
            }

            Entities = areaEntities;
        }
    }
}
