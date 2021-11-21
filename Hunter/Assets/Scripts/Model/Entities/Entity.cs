using System;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Entity
    {
        public float BodyRadius { get; set; }
        public Vector2 Velocity { get; set; } = new Vector2(1, 0);
        public Vector2 Acceleraion { get; set; }
        public Vector2 Position { get; set; }
        public float MaxSpeed { get; set; } = 3 * 0.001f;
        float wanderAngle = 10;

        public void Move()
        {
            Vector2 steering = Wander();
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }

        private Vector2 Wander()
        {
            Vector2 circleCenter = Velocity;
            circleCenter = Vector2.Normalize(circleCenter);
            float circleDistance = 10;
            circleCenter *= circleDistance;

            Console.WriteLine(circleCenter);

            float circleRadius = 1;
            Vector2 displacement = new Vector2(0, -1);
            displacement *= circleRadius;

            float vectorlength = displacement.Length();
            displacement.X = (float)Math.Cos(wanderAngle * vectorlength);
            displacement.Y = (float)Math.Sin(wanderAngle * vectorlength);

            Console.WriteLine(displacement);

            Random random = new Random();
            float angleChange = 1;
            wanderAngle += random.Next(-3, 3) * angleChange - angleChange * 0.5f;

            Vector2 wanderForce = Vector2.Add(circleCenter, displacement);

            //Console.WriteLine(wanderForce);

            //Acceleraion = Vector2.Add(wanderForce, Acceleraion);
            //Velocity = Vector2.Add(Acceleraion, Velocity);
            //Velocity = Vector2.Multiply(Velocity, MaxSpeed);
            //Velocity = Vector2.Multiply(Velocity, wanderForce);
            //Position = Vector2.Add(Position, Velocity);

            //Acceleraion = Vector2.Zero;
            //float xPos = (float)random.NextDouble() + 0.01f;
            //float yPos = (float)random.NextDouble() + 0.01f;
            //Velocity = new Vector2(xPos, yPos);

            return wanderForce;
        }

        public void Die() { }
    }
}
