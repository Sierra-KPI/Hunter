using System.Numerics;

namespace HunterModel
{
    public class HunterGame
    {
        public List<Rabbit> Rabbits = new();

        public HunterGame(int rabbits, int deers, int wolfs)
        {
            for (var i = 0; i < rabbits; i++)
            {
                int xPos = new Random().Next(0, 11);
                int yPos = new Random().Next(0, 11);
                Rabbit rabbit = new Rabbit
                {
                    Position = new Vector2(xPos, yPos)
                };
                Rabbits.Add(new Rabbit());
                Console.WriteLine(rabbit.Position);
            }

            TimerCallback timerCallback = new TimerCallback(Update);
            Timer timer = new(timerCallback, null, 0, 2000);
            Console.ReadLine();
        }

        public void Update(object obj)
        {
            foreach (Rabbit rabbit in Rabbits)
            {
                rabbit.Move();
            }
        }
    }
}
