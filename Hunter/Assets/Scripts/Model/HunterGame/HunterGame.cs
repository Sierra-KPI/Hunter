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
        private readonly float _deadBorder = 5f;

        public HunterGame(int rabbits, int deers, int wolfs)
        {
            Entities.Add(AnimalType.Rabbit, Rabbit.CreateEntities(rabbits));
            Entities.Add(AnimalType.Deer, Herd.CreateEntities(deers));

            Hunter = new HunterPlayer();
        }

        public void Update()
        {
            foreach (AnimalType animalType in
                (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                foreach (Animal animal in Entities[animalType])
                {
                    animal.Move();
                    animal.GetEntitiesInArea(Entities.SelectMany(d =>
                    d.Value).ToList());

                    if (animal.IsBehindBoard(_deadBorder))
                    {
                        KillAnimal(animal);
                    }
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

        public Animal TryToKillAnimalByShot(float shotX, float shotY)
        {
            var shot = new Vector2(shotX, shotY);
            var shotVector = shot - Hunter.Position;

            foreach (AnimalType animalType in
                (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                var list = GetAnimals(animalType);
                foreach (Animal animalEntity in list)
                {
                    var animalVector = (animalEntity.Position - Hunter.Position);
                    if (animalVector.Length() > Hunter.ShotDistance)
                    {
                        continue;
                    }

                    double maxAngle = Math.Asin(animalEntity.BodyRadius /
                        animalVector.Length());

                    double shotAngle = Math.Acos((shotVector.X *
                        animalVector.X + shotVector.Y * animalVector.Y) / (
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

        public bool KillAnimal(Animal animal)
        {
            switch (animal.AnimalType)
            {
                case AnimalType.Rabbit:
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
