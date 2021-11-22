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
        public float WanderAngle { get; set; }

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

            WanderAngle = random.Next(-1, 1);
            Velocity = new Vector2(xPos, yPos);
        }
    }
}
