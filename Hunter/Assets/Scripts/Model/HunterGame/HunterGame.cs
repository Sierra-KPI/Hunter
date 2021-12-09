using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public Dictionary<AnimalType, List<Entity>> Entities = new();
        public HunterPlayer Hunter;

        public HunterGame(int rabbits, int deers, int wolves)
        {
            Entities.Add(AnimalType.Rabbit, Rabbit.CreateEntities(rabbits));
            Entities.Add(AnimalType.Deer, Herd.CreateEntities(deers));
            Entities.Add(AnimalType.Wolf, Wolf.CreateEntities(wolves));

            Hunter = new HunterPlayer();
        }

        public void Update()
        {
            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                foreach (Animal animal in Entities[animalType])
                {
                    animal.Move();
                    //animal.GetEntititesInArea(Entities.SelectMany(d => d.Value).ToList());
                    animal.GetEntititesInArea(GetAllEntities());
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
                case AnimalType.Wolf:
                    entities = Entities[animalType];
                    break;
            }
            return entities;
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> entities = new();
            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                entities.AddRange(GetAnimals(animalType));
            }
            //entities.Add(Hunter);
            return entities;
        }

        public Animal TryToKillAnimalByShot(float shotX, float shotY)
        {
            var shot = new Vector2(shotX, shotY);
            var shotVector = shot - Hunter.Position;

            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                var list = GetAnimals(animalType);
                foreach (Animal animalEntity in list)
                {
                    var animalVector = (animalEntity.Position - Hunter.Position);
                    if (animalVector.Length() > Hunter.ShotDistance) continue;

                    double maxAngle = Math.Asin(animalEntity.BodyRadius / animalVector.Length());

                    double shotAngle = Math.Acos((shotVector.X * animalVector.X + shotVector.Y * animalVector.Y) / (
                        shotVector.Length() * animalVector.Length()));

                    maxAngle = TransformAngle(maxAngle);
                    shotAngle = TransformAngle(shotAngle);

                    if (Math.Abs(maxAngle) >= Math.Abs(shotAngle))
                    {
                        if (KillAnimal(animalEntity))
                        {
                            return animalEntity;
                        }
                    }
                }
            }
            return null;
        }

        public Animal TryToKillAnimalByWolf()
        {
            foreach (Animal wolf in Entities[AnimalType.Wolf])
            {
                foreach (Animal animal in wolf.Entities)
                {
                    if (animal.AnimalType == AnimalType.Wolf) continue;
                    if (CollisionDetection.AreColliding(wolf, animal, wolf.BodyRadius, animal.BodyRadius))
                    {
                        if (KillAnimal(animal))
                        {
                            return animal;
                        }
                    }
                }
            }
            return null;
        }

        private bool KillAnimal(Animal animal)
        {
            switch (animal.AnimalType)
            {
                case AnimalType.Rabbit:
                case AnimalType.Wolf:
                    return Entities[animal.AnimalType].Remove(animal);
                case AnimalType.Deer:
                    foreach (Herd herd in Entities[animal.AnimalType])
                    {
                        if (herd.RemoveAnimal((HerdAnimal)animal))
                        {
                            if (herd.GetAnimals().GetLength(0) == 0)
                            {
                                Entities[animal.AnimalType].Remove(herd);
                            }
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public bool TryToKillHunter()
        {
            foreach (Animal wolf in Entities[AnimalType.Wolf])
            {
                if (CollisionDetection.AreColliding(wolf, Hunter, wolf.BodyRadius, Hunter.BodyRadius))
                {
                    return true;
                }
            }
            return false;
        }


        private double TransformAngle(double angle)
        {
            if (angle > Math.PI)
            {
                return -(2 * Math.PI - angle);
            }
            else
            {
                return angle;
            }
        }

    }
}
