using System;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Deer : HerdAnimal
    {
        public float RunSpeed { get; set; }

        public Deer() : base()
        {
            MaxSpeed = 1f * 0.001f;
        }

        public override void MoveInHerd(HerdAnimal[] Deers)
        {
            Vector2 wander = Behaviours.Wander(this);
            Vector2 steering = Behaviours.GetHerdVelocity(Deers, this);
            Velocity = Vector2.Multiply(Velocity + steering + wander, MaxSpeed);
            Position += Velocity;
        }

    }
}
