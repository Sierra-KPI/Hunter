using System;
using System.Numerics;

namespace Hunter.Model.Entities
{
    class Deer : Animal
    {
        public float RunSpeed { get; set; }

        public Deer() : base()
        {
            MaxSpeed = 1f * 0.001f;
        }

        public override void Move()
        {
            Vector2 steering = Behaviours.Wander(this);
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }

    }
}
