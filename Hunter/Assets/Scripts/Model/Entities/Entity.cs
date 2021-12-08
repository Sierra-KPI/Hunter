using System;
using System.Numerics;
using System.Timers;

namespace Hunter.Model.Entities
{
    public abstract class Entity
    {
        public float BodyRadius { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleraion { get; set; }
        public Vector2 Position { get; set; }
        public float MaxSpeed { get; set; }

        public abstract void Move();

        public void Die() { }
    }
}
