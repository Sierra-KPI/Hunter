using System.Numerics;

namespace HunterModel
{
    public class Entity
    {
        public float BodyRadius { get; }
        public float MoveSpeed { get; }
        public Vector2 Position { get; }

        public void Move() { }
        public void Die() { }
    }
}
