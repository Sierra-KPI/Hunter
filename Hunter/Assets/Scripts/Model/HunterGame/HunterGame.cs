using System;
using System.Collections.Generic;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public List<Rabbit> Rabbits = new();
        public List<Herd> HerdsOfDeer = new();

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

            for (var i = 0; i < deers; i++)
            {
                int xPos = new Random().Next(-3, 3);
                int yPos = new Random().Next(-3, 3);

                Herd herd = new Herd
                {
                    Position = new Vector2(xPos, yPos)
                    //Position = Vector2.Zero
                };

                HerdsOfDeer.Add(herd);
            }
        }
    
        public void Update()
        {
            foreach (Rabbit rabbit in Rabbits)
            {
                rabbit.Move();
            }
            foreach (Herd herd in HerdsOfDeer)
            {
                herd.Move();
            }

        }
    }
}
