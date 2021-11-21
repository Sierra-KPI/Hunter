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
                int xPos = new Random().Next(0, 11);
                int yPos = new Random().Next(0, 11);
                Rabbit rabbit = new Rabbit
                {
                    Position = new Vector2(xPos, yPos)
                };
                Rabbits.Add(new Rabbit());
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
