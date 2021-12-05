using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public Dictionary<AnimalType, List<Entity>> Entities = new();

        public HunterGame(int rabbits, int deers, int wolfs)
        {
            Entities.Add(AnimalType.Rabbit, Rabbit.CreateEntities(rabbits));
            Entities.Add(AnimalType.Deer, Herd.CreateEntities(deers));
        }

        public void Update()
        {
            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                foreach (Animal animal in Entities[animalType])
                {
                    animal.Move();
                    animal.GetEntititesInArea(Entities.SelectMany(d => d.Value).ToList());
                }
            }
        }

        public List<Entity> GetAnimals(AnimalType animalType)
        {
            List<Entity> entities = new();
            switch (animalType)
            {
                case AnimalType.Rabbit:
                    entities = Entities[animalType];
                    break;
                case AnimalType.Deer:
                    foreach (Herd herd in Entities[animalType])
                    {
                        foreach (Animal anim in herd.GetAnimals())
                        {
                            entities.Add(anim);
                        }
                    }
                    break;
            }
            return entities;
        }
    }
}
