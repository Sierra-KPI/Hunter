﻿using System.Numerics;

namespace Hunter.Model.Entities
{
    public class HunterPlayer : Entity
    {
        private int _bullets = 200;
        public float ShotDistance = 3f;

        public HunterPlayer()
        {
            MaxSpeed = 1 * 0.01f;
        }


        public override void Move()
        {
        }

        public void MoveTo(float h, float v)
        {
            Vector2 vector = new Vector2(h, v);
            Velocity = Vector2.Multiply(vector, MaxSpeed);
            Position += Velocity;
        }

        public bool HasBullets()
        {
            if (_bullets > 0) return true;
            return false;
        }

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