using System;
using System.Collections.Generic;
using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Wolf : Animal
    {
        public float RunSpeed { get; set; }
        public int Hunger { get; set; }
        public int maxHunger = 5000;

        public Wolf() : base()
        {
            MaxSpeed = 1.5f * 0.001f;
            BodyRadius = 0.5f;
            BodySeekRadius = 4;
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;
            Hunger = maxHunger;

            EntityType = EntityType.Wolf;
        }

        public static List<Entity> CreateEntities(int numberOfWolves)
        {
            var wolves = new List<Entity>();
            for (var i = 0; i < numberOfWolves; i++)
            {
                int xPos = new Random().Next(-18, 18);
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
            CheckWolfHunger();
            Vector2 wander = WanderBehaviour.Wander(this);
            Vector2 chasing = PursueBehaviour.Chase(this);
            Vector2 borderAvoidence = AvoidBordersBehaviour.AvoidBorders(this);
            Velocity = Vector2.Multiply(Velocity + wander + chasing, MaxSpeed);
            Velocity = Vector2.Multiply(Velocity + borderAvoidence, MaxSpeed * 600);
            Position += Velocity;
        }


        private void CheckWolfHunger()
        {
            if (Hunger <= 0)
            {
                IsDead = true;
            }
            Hunger -= 5;
        }
    }
}

