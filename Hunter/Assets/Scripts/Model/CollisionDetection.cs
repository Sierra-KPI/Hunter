using Hunter.Model.Entities;

namespace Hunter.Model
{
    public static class CollisionDetection
    {
        public static bool AreColliding(Entity firstEntity,
            Entity secondEntity, float firstRadius, float secondRadius)
        {
            var radius = firstRadius + secondRadius;

            var deltaX = firstEntity.Position.X - secondEntity.Position.X;
            var deltaY = firstEntity.Position.Y - secondEntity.Position.Y;

            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }
    }
}
