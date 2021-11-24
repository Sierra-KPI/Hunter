﻿using System;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Deer : Animal
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

        public void Move(Deer[] Deers)
        {
            Vector2 wander = Behaviours.Wander(this);
            Vector2 steering = Behaviours.GetHerdVelocity(Deers, this);
            Velocity = Vector2.Multiply(Velocity + steering + wander, MaxSpeed);
            Position += Velocity;
        }

    }
}
