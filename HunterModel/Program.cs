using System.Numerics;

namespace HunterModel
{
    public class Program
    {
        public static void Main()
        {
            Entity entity = new Entity();
            Entity entity2 = new Entity();

            HunterGame game = new(10, 0, 0);
            //entity.Position = new Vector2(1, 1);
            //entity2.Position = new Vector2(3, 4);
            //Console.WriteLine(Vector2.Add(entity.Position, entity2.Position));
        }
    }
}
