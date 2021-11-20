using System.Numerics;

namespace HunterModel
{
    public class Entity
    {
        public float BodyRadius { get; set; }
        public float MoveSpeed { get; set; }
        public Vector2 Position { get; set; }

        public void Move() { }
        public void Die() { }
    }
}
