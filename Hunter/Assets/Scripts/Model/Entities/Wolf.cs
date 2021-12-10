using System;
using System.Collections.Generic;
using System.Numerics;
using Hunter.Model.Behaviours;
using System.Timers;

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
            //Hunger.Elapsed += wolf.Die();
        }

        public Wolf() : base()
        {
            MaxSpeed = 1f * 0.001f;
            BodyRadius = 0.5f;
            BodySeekRadius = 5;
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
            Vector2 chasing = PursueBehaviour.Chase(this);
            Velocity = Vector2.Multiply(Velocity + steering + chasing, MaxSpeed);
            Position += Velocity;
        }
    }
}

