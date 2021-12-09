using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Hunter.Model.Entities;
using UnityEngine;

namespace Hunter.Model.HunterGame
{
    public class HunterGame
    {
        public Dictionary<AnimalType, List<Entity>> Entities = new();
        public HunterPlayer Hunter;

        public HunterGame(int rabbits, int deers, int wolfs)
        {
            Entities.Add(AnimalType.Rabbit, Rabbit.CreateEntities(rabbits));
            Entities.Add(AnimalType.Deer, Herd.CreateEntities(deers));

            Hunter = new HunterPlayer();
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

        public void TryToKillAnimalByShot(float shotX, float shotY)
        {
            var shot = new System.Numerics.Vector2(shotX, shotY);
            var shotVector = shot - Hunter.Position;

            foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
            {
                var list = GetAnimals(animalType);
                foreach (Animal animalEntity in list)
                {
                    var animalVector = (animalEntity.Position - Hunter.Position);
                    if (animalVector.Length() > Hunter.ShotDistance) continue;
                    Debug.Log("KillAnimalByShot");

                    double maxAngle = Math.Asin(animalEntity.BodyRadius / animalVector.Length());

                    double shotAngle = Math.Acos((shotVector.X * animalVector.X + shotVector.Y * animalVector.Y) / (
                        shotVector.Length() * animalVector.Length()));

                    maxAngle = TransformAngle(maxAngle);
                    shotAngle = TransformAngle(shotAngle);

                    if (Math.Abs(maxAngle) >= Math.Abs(shotAngle))
                    {
                        Debug.Log(animalType + "Kill");
                    }

                    
                }
            }

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
