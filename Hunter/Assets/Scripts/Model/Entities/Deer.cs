using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Deer : HerdAnimal
    {
        //public float WolfRadius { get; set; }

        public Deer() : base()
        {
            MaxSpeed = 1f * 0.001f;
            BodyRadius = 0.34f;
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;

            AnimalType = AnimalType.Deer;
        }

        public override void MoveInHerd(HerdAnimal[] Deers)
        {
            Vector2 wander = WanderBehaviour.Wander(this);
            Vector2 steering = HerdBehaviour.GetHerdVelocity(Deers, this);
            Vector2 fleeing = FleetBehaviour.DeerRunAway(this);
            if (Deers.GetLength(0) == 1) Velocity = Vector2.Multiply(Velocity + wander + fleeing, MaxSpeed);
            else Velocity = Vector2.Multiply(Velocity + steering + wander + fleeing, MaxSpeed);
            Position += Velocity;
        }

    }
}
