using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hunter.Model.Entities
{
    public class Herd : Animal
    {
        private List<HerdAnimal> _animals;

        public Herd()
        {
            int numberOfAnimals = new Random().Next(3, 10);
            _animals = new List<HerdAnimal>();

            int xPos = new Random().Next(-18, 18);
            int yPos = new Random().Next(-8, 8);

            for (var i = 0; i < numberOfAnimals; i++)
            {
                _animals.Add(new Deer { Position = new Vector2(xPos, yPos) });
            }
        }

        public static List<Entity> CreateEntities(int numberOfHerds)
        {
            var herds = new List<Entity>();
            for (var i = 0; i < numberOfHerds; i++)
            {
                Herd herd = new Herd();
                herds.Add(herd);
            }
            return herds;
        }

        public override void Move()
        {
            foreach (HerdAnimal deer in _animals)
            {
                deer.MoveInHerd(_animals.ToArray());
            }
        }

        public HerdAnimal[] GetAnimals() => _animals.ToArray();

        public bool RemoveAnimal(HerdAnimal animal)
        {
            return _animals.Remove(animal);
        }

    }
}
