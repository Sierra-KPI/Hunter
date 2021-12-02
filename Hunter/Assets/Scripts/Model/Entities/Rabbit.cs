using System.Numerics;
using Hunter.Model.Behaviours;

namespace Hunter.Model.Entities
{
    public class Rabbit : Animal
    {
        public float RunSpeed { get; set; }

        public Rabbit() : base()
        {
            MaxSpeed = 2 * 0.001f
            BodyRadius = 0.25f; // If size in unity = 3,
                                // then divide it by 12 and
                                // You'll get BodyRadius
            WanderCircleDistance = 10;
            WanderCircleRadius = 4;
            MaxWanderShift = 3;
        }

        public override void Move()
        {
            Vector2 steering = WanderBehaviour.Wander(this);
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }
    }
}
