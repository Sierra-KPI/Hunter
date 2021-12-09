using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Deer : HerdAnimal
    {
        public float RunSpeed { get; set; }

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
            Velocity = Vector2.Multiply(Velocity + steering + wander, MaxSpeed);
            Position += Velocity;
        }

    }
}
