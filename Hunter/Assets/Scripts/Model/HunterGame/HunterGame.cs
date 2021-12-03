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


        public List<Animal> GetAnimals(AnimalType animalType)
        {
            List<Animal> animals = new();
            switch (animalType)
            {
                case AnimalType.Rabbit:
                    animals = Animals[animalType];
                    break;
                case AnimalType.Deer:
                    foreach (Herd herd in Animals[animalType])
                    {
                        foreach (Animal anim in herd.GetAnimals())
                        {
                            animals.Add(anim);
                        }
                    }
                    break;
            }
            return animals;
        }

    }
}
