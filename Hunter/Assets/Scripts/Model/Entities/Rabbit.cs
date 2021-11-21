using System;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Rabbit : Animal
    {
        public float RunSpeed { get; set; }
        public float MaxSpeed =  3 * 0.001f;

        private float _wanderAngle = 10;

        public Rabbit()
        {
            Random random = new Random();
            float max = 1;
            float min = -1;
            float xPos = (float)(random.NextDouble() * (max - min) + min);
            float yPos = (float)(random.NextDouble() * (max - min) + min);
            if (xPos == 0)
            {
                xPos += 0.1f;
            }
            if (yPos == 0)
            {
                yPos += 0.1f;
            }
            Velocity = new Vector2(xPos, yPos);
        }

        public override void Move()
        {
            Vector2 steering = Wander();
            Velocity = Vector2.Multiply(Velocity + steering, MaxSpeed);
            Position += Velocity;
        }

        private Vector2 Wander()
        {
            Random random = new Random();

            Vector2 circleCenter = Velocity;
            circleCenter = Vector2.Normalize(circleCenter);
            float circleDistance = 10;
            circleCenter *= circleDistance;

            float circleRadius = 1;
            Vector2 displacement = new Vector2(0, -1);
            displacement *= circleRadius;

            float vectorlength = displacement.Length();
            displacement.X = (float)Math.Cos(_wanderAngle * vectorlength);
            displacement.Y = (float)Math.Sin(_wanderAngle * vectorlength);

            float angleChange = 1;
            _wanderAngle += random.Next(-3, 3) * angleChange - angleChange * 0.5f;

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
    }
}
