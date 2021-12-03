using System;
using System.Collections.Generic;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public Dictionary<AnimalType, List<Animal>> Animals = new();

        public HunterGame(int rabbits, int deers, int wolfs)
        {
            Animals.Add(AnimalType.Rabbit, Rabbit.CreateAnimals(rabbits));
            Animals.Add(AnimalType.Deer, Herd.CreateAnimals(deers));

        }

        public void Update()
        {
            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                foreach (Animal animal in Animals[animalType])
                {
                    animal.Move();
                }
            }
        }
    }
}
