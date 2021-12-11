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
            RunSpeed = 4 * 0.001f;
            BodyRadius = 0.25f; // If size in unity = 3,
                                // then divide it by 12 and
                                // You'll get BodyRadius
            BodySeekRadius = 3; // if seek radius in AnimalGizmos
                                // is 5, then divide it by 1,6
                                // and You'll get BodySeekRadius
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;

            EntityType = EntityType.Rabbit;
        }

        public static List<Entity> CreateEntities(int numberOfRabbits)
        {
            var rabbits = new List<Entity>();
            for (var i = 0; i < numberOfRabbits; i++)
            {
                int xPos = new Random().Next(-8, 8);
                int yPos = new Random().Next(-8, 8);

                Rabbit rabbit = new Rabbit
                {
                    Position = new Vector2(xPos, yPos)
                };

                rabbits.Add(rabbit);
            }

            return rabbits;
        }

        public override void Move()
        {
            Vector2 wander = WanderBehaviour.Wander(this);
            Vector2 borderAvoidence = AvoidBordersBehaviour.AvoidBorders(this);
            Vector2 fleeing = FleeBehaviour.RunAway(this);
            Velocity = Vector2.Multiply(Velocity + wander + fleeing, MaxSpeed);
            Velocity = Vector2.Multiply(Velocity +
                borderAvoidence, MaxSpeed * 600);
            //Velocity = Vector2.Multiply(Velocity + fleeing, RunSpeed);

            Position += Velocity;
        }
    }
}
