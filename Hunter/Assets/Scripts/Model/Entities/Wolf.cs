using System;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Wolf : Animal
    {
        public float RunSpeed { get; set; }

        public static void lifeSpan()
        {
            Wolf wolf = new Wolf();
            Timer Hunger = new System.Timers.Timer();
            Hunger.Interval = 5000;
            Hunger.AutoReset = false;
            Hunger.Enabled = true;
            //Hunger.Elapsed += DieFromHunger;
        }

        //private static Wolf DieFromHunger(Wolf wolf, bool toDie)
        //{
        //    if (KillAnimal(wolf))
        //    {
        //        return wolf;
        //    }
        //}

        public Wolf() : base()
        {
            MaxSpeed = 1.5f * 0.001f;
            BodyRadius = 0.5f;
            BodySeekRadius = 4;
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;

            EntityType = EntityType.Wolf;
        }

        public static List<Entity> CreateEntities(int numberOfWolves)
        {
            var wolves = new List<Entity>();
            for (var i = 0; i < numberOfWolves; i++)
            {
                int xPos = new Random().Next(-8, 8);
                int yPos = new Random().Next(-8, 8);

                Wolf wolf = new Wolf
                {
                    Position = new Vector2(xPos, yPos)
                };

                wolves.Add(wolf);
            }
            return wolves;
        }

        public override void Move()
        {
            Vector2 wander = WanderBehaviour.Wander(this);
            Vector2 chasing = PursueBehaviour.Chase(this);
            Vector2 borderAvoidence = AvoidBordersBehaviour.AvoidBorders(this);
            Velocity = Vector2.Multiply(Velocity + wander + chasing, MaxSpeed);
            Velocity = Vector2.Multiply(Velocity + borderAvoidence, MaxSpeed * 600);
            Position += Velocity;
        }
    }
}

