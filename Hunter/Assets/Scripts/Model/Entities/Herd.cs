using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Herd : Animal
    {
        private HerdAnimal[] _animals;

        public Herd()
        {
            int numberOfAnimals = new Random().Next(3, 10);
            _animals = new Deer[numberOfAnimals]; // to fix
            for (var i = 0; i < numberOfAnimals; i++)
            {
                _animals[i] = new Deer();
            }

        }

        public static List<Animal> CreateAnimals(int numberOfHerds)
        {
            var herds = new List<Animal>();
            for (var i = 0; i < numberOfHerds; i++)
            {
                int xPos = new Random().Next(-3, 4);
                int yPos = new Random().Next(-3, 4);

                Herd herd = new Herd
                {
                    //Position = new Vector2(xPos, yPos)
                    Position = Vector2.Zero
                };

                herds.Add(herd);
            }
            return herds;
        }

        public override void Move()
        {
            foreach (HerdAnimal deer in _animals)
            {
                deer.MoveInHerd(_animals);
            }
        }

        public HerdAnimal[] GetAnimals() => _animals;

    }
}
