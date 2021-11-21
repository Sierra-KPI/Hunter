using System.Numerics;

namespace HunterModel
{
    public class Entity
    {
        public float BodyRadius { get; set; }
        public Vector2 Velocity { get; set; } = new Vector2(1, 0);
        public Vector2 Acceleraion { get; set; }
        public Vector2 Position { get; set; }
        public float MaxSpeed { get; set; } = 4;
        public float MaxForce { get; set; } = 0.2f;

        public void Move()
        {
            Random random = new Random();
            int max = 10;
            int min = 0;
            float xPos = (float)(random.NextDouble() * (max - min) + min);
            float yPos = (float)(random.NextDouble() * (max - min) + min);
            Vector2 randVector = new Vector2(xPos, yPos);
            Acceleraion = Vector2.Add(randVector, Acceleraion);
            Velocity = Vector2.Add(Acceleraion, Velocity);
            Position = Acceleraion;
            //Console.WriteLine(Position);
            Acceleraion = Vector2.Zero;
        }

        public void Die() { }
    }
}
