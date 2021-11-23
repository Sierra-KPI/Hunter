using System;
using System.Collections.Generic;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public List<Rabbit> Rabbits = new();
    
        public HunterGame(int rabbits, int deers, int wolfs)
        {
            for (var i = 0; i < rabbits; i++)
            {
                int xPos = new Random().Next(-3, 4);
                int yPos = new Random().Next(-3, 4);

                Rabbit rabbit = new Rabbit
                {
                    //Position = new Vector2(xPos, yPos)
                    Position = Vector2.Zero
                };

                Rabbits.Add(rabbit);
            }
        }
    
        public void Update()
        {
            foreach (Rabbit rabbit in Rabbits)
            {
                rabbit.Move();
            }
        }
    }
}
