using System.Numerics;

namespace Hunter.Model.Entities
{
    public abstract class Entity
    {
        public float BodyRadius { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleraion { get; set; }
        public Vector2 Position { get; set; }
        public float MaxSpeed { get; set; }

        public EntityType EntityType;

        public abstract void Move();
        public bool IsDead { get; set; }

        public bool IsBehindBoard(float board)
        {
            if (Position.X + BodyRadius > board * 2 ||
                Position.X + BodyRadius < -board * 2 ||
                Position.Y + BodyRadius > board ||
                Position.Y + BodyRadius < -board)
            {
                return true;
            }

            return false;
        }

    }
}
