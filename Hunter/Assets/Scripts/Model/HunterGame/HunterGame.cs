using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Hunter.Model.Entities;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public Dictionary<EntityType, List<Entity>> Entities = new();
        public HunterPlayer Hunter;
        private readonly float _deadBorder = 8f; // CHANGE

        public HunterGame(int rabbits, int deers, int wolves)
        {
            Entities.Add(EntityType.Rabbit, Rabbit.CreateEntities(rabbits));
            Entities.Add(EntityType.Deer, Herd.CreateEntities(deers));
            Entities.Add(EntityType.Wolf, Wolf.CreateEntities(wolves));

            Hunter = new HunterPlayer();
        }

        public void Update()
        {
            foreach (EntityType entityType in
                (EntityType[])Enum.GetValues(typeof(EntityType)))
            {
                if (entityType == EntityType.Hunter) continue;
                List<Animal> animalsToBeKilled = new List<Animal>();
                foreach (Animal animal in Entities[entityType])
                {
                    animal.Move();
                    animal.GetEntitiesInArea(GetAllEntities());

                    if (animal.IsBehindBoard(_deadBorder))
                    {
                        animalsToBeKilled.Add(animal);
                        animal.IsDead = true;
                    }

                    if (entityType == EntityType.Deer)
                    {
                        foreach (Animal herdAnimal in ((Herd)animal).GetAnimals())
                        {
                            if (herdAnimal.IsBehindBoard(_deadBorder))
                            {
                                animalsToBeKilled.Add(herdAnimal);
                                herdAnimal.IsDead = true;
                            }
                        }
                    }
                }

                foreach (Animal animal in animalsToBeKilled)
                {
                    KillAnimal(animal);
                }
            }
        }

        public List<Entity> GetAnimals(EntityType entityType)
        {
            List<Entity> entities = new();
            switch (entityType)
            {
                case EntityType.Rabbit:
                    entities = Entities[entityType];
                    break;
                case EntityType.Deer:
                    foreach (Herd herd in Entities[entityType])
                    {
                        foreach (Animal anim in herd.GetAnimals())
                        {
                            entities.Add(anim);
                        }
                    }
                    break;
                case EntityType.Wolf:
                    entities = Entities[entityType];
                    break;
            }
            return entities;
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> entities = new();
            foreach (EntityType animalType in (EntityType[])Enum.GetValues(typeof(EntityType)))
            {
                entities.AddRange(GetAnimals(animalType));
            }
            entities.Add(Hunter);
            return entities;
        }

        public Animal TryToKillAnimalByHunter(float shotX, float shotY)
        {
            var shot = new Vector2(shotX, shotY);
            var shotVector = shot - Hunter.Position;

            foreach (EntityType animalType in
                (EntityType[])Enum.GetValues(typeof(EntityType)))
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
                            animalEntity.IsDead = true;
                            return animalEntity;
                        }
                    }
                }
            }
            return null;
        }

        public Animal TryToKillAnimalByWolf()
        {
            foreach (Animal wolf in Entities[EntityType.Wolf])
            {
                foreach (Entity animal in wolf.Entities)
                {
                    if (animal is HunterPlayer) continue;
                    if (((Animal)animal).EntityType == EntityType.Wolf) continue;
                    if (CollisionDetection.AreColliding(wolf, animal, wolf.BodyRadius, animal.BodyRadius))
                    {
                        if (KillAnimal((Animal)animal))
                        {
                            return (Animal)animal;
                        }
                    }
                }
            }
            return null;
        }

        private bool KillAnimal(Animal animal)
        {
            switch (animal.EntityType)
            {
                case EntityType.Rabbit:
                case EntityType.Wolf:
                    return Entities[animal.EntityType].Remove(animal);
                case EntityType.Deer:
                    foreach (Herd herd in Entities[animal.EntityType])
                    {
                        if (herd.RemoveAnimal((HerdAnimal)animal))
                        {
                            if (herd.GetAnimals().GetLength(0) == 0)
                            {
                                Entities[animal.EntityType].Remove(herd);
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
            foreach (Animal wolf in Entities[EntityType.Wolf])
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
