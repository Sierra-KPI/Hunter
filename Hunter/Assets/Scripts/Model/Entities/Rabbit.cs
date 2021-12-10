using System;
using System.Collections.Generic;
using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Rabbit : Animal
    {
        public float RunSpeed { get; set; }

        public Rabbit() : base()
        {
            MaxSpeed = 2 * 0.001f;
            BodyRadius = 0.25f; // If size in unity = 3,
                                // then divide it by 12 and
                                // You'll get BodyRadius
            BodySeekRadius = 3; // if seek radius in AnimalGizmos
                                // is 5, then divide it by 1,6
                                // and You'll get BodySeekRadius
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;

            AnimalType = AnimalType.Rabbit;
        }

        public static List<Entity> CreateEntities(int numberOfRabbits)
        {
            var rabbits = new List<Entity>();
            for (var i = 0; i < numberOfRabbits; i++)
            {
                int xPos = new Random().Next(-3, 4);
                int yPos = new Random().Next(-3, 4);

                Rabbit rabbit = new Rabbit
                {
                    //Position = new Vector2(xPos, yPos)
                    Position = Vector2.Zero
                };

                rabbits.Add(rabbit);
            }

            return rabbits;
        }

        public override void Move()
        {
            Vector2 steering = WanderBehaviour.Wander(this);
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }
    }
}
