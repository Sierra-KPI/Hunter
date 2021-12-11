using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Deer : HerdAnimal
    {
        public float WolfRadius { get; set; }

        public Deer() : base()
        {
            MaxSpeed = 1f * 0.001f;
            BodyRadius = 0.34f;
            BodySeekRadius = 4;
            WolfRadius = 5;
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;

            EntityType = EntityType.Deer;
        }

        public override void MoveInHerd(HerdAnimal[] Deers)
        {
            Vector2 wander = WanderBehaviour.Wander(this);
            Vector2 borderAvoidence = AvoidBordersBehaviour.AvoidBorders(this);
            Vector2 herdBehaviour = HerdBehaviour.GetHerdVelocity(Deers, this);
            Vector2 fleeing = FleeBehaviour.RunAway(this);
            if (Deers.GetLength(0) == 1)
            {
                Velocity = Vector2.Multiply(Velocity + wander + fleeing, MaxSpeed);
            }
            else
            {
                Velocity = Vector2.Multiply(Velocity + herdBehaviour + wander + fleeing, MaxSpeed);
            }
            Velocity = Vector2.Multiply(Velocity + borderAvoidence, MaxSpeed * 600);
            Position += Velocity;
        }

    }
}
