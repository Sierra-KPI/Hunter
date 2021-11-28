using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Rabbit : Animal
    {
        public float RunSpeed { get; set; }

        public Rabbit() : base()
        {
            MaxSpeed = (float)(0.1 * 0.001f);
            BodyRadius = 0.25f; // If size in unity = 3,
                                // then divide it by 12 and
                                // You'll get BodyRadius
        }

        public override void Move()
        {
            Vector2 steering = Behaviours.Wander(this);
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }
    }
}
