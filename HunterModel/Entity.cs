using System.Numerics;

namespace HunterModel
{
    public class Entity
    {
        public float BodyRadius { get; set; }
        public Vector2 Velocity { get; set; } = new Vector2(1, 0);
        public Vector2 Acceleraion { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; } = 4;
        public float Force { get; set; } = 0.2f;
        public float WanderTheta { get; set; } = (float)(Math.PI / 2);

        public void Move() { }
        public void Die() { }
    }
}
