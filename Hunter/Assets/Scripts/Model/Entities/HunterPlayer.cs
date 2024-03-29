﻿using System.Numerics;

namespace Hunter.Model.Entities
{
    public class HunterPlayer : Entity
    {
        private int _bullets = 40;
        public float ShotDistance = 3f;

        public HunterPlayer()
        {
            MaxSpeed = 1.3f * 0.01f;
            EntityType = EntityType.Hunter;
            BodyRadius = 0.3f;
        }

        public override void Move() { }

        public void MoveTo(float h, float v)
        {
            Vector2 vector = new Vector2(h, v);
            Velocity = Vector2.Multiply(vector, MaxSpeed);
            Position += Velocity;
        }

        public bool HasBullets() => _bullets > 0;

        public bool MakeShot()
        {
            if (HasBullets())
            {
                _bullets--;
                return true;
            }

            return false;
        }
    }
}
