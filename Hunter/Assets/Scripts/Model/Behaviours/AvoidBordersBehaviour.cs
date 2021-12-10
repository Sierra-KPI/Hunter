using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public static class AvoidBordersBehaviour
    {
        private static readonly float s_safeBorder = 7.2f; // CHANGE

        public static Vector2 AvoidBorders(Animal animal)
        {
            if (animal.Position.X > s_safeBorder)
            {
                return new Vector2(-animal.MaxSpeed, 0);
            }
            if (animal.Position.Y > s_safeBorder)
            {
                return new Vector2(0, -animal.MaxSpeed);
            }
            if (animal.Position.X < -s_safeBorder)
            {
                return new Vector2(animal.MaxSpeed, 0);
            }
            if (animal.Position.Y < -s_safeBorder)
            {
                return new Vector2(0, animal.MaxSpeed);
            }

            return Vector2.Zero;
        }
    }
}
