using System;
using System.Collections.Generic;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{

    public class Wolf : Animal
    {
        public float RunSpeed { get; set; }

        public Wolf() : base()
        {
            MaxSpeed = 1.5f * 0.001f;
            BodyRadius = ;
            BodySeekRadius;
            PreySeekRadius = ;
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;
        }

        public static List<Entity> CreateEntities(int numberOfWolves)
        {
            var wolves = new List<Entity>();
            for (var i = 0; i < numberOfWolves; i++)
            {
                int xPos = new Random().Next(-3, 4);
                int yPos = new Random().Next(-3, 4);

                Wolf wolf = new Wolf
                {
                    //Position = new Vector2(xPos, yPos)
                    Position = Vector2.Zero
                };

                wolves.Add(wolf);
            }
            return wolves;
        }

        public override void Move()
        {
            Vector2 steering = WanderBehaviour.Wander(this);
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }
    }
}
