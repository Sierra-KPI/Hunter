public class HunterGame
{
    public List<Rabbit> Rabbits = new();

    public HunterGame(int rabbits, int deers, int wolfs)
    {
        for (var i = 0; i < rabbits; i++)
        {
            Rabbit rabbit = new Rabbit
            {
                Position = new System.Numerics.Vector2 { }
            };
            Rabbits.Add(new Rabbit());
        }
    }
}
