﻿using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class EntityObject
{
    public AnimalType AnimalType;
    public GameObject Prefab;
    public int Number;

    public EntityObject(AnimalType animal, GameObject prefab)
    {
        AnimalType = animal;
        Prefab = prefab;
        Number = EntityFactory.GetAnimalsNumber(animal);
    }
}

public class EntityFactory : MonoBehaviour
{
    public static Dictionary<string, int> AnimalsNumber = new();

    public List<EntityObject> EntityObjects;
    public Dictionary<AnimalType, Queue<GameObject>> EntityDictionary = new();

    public static EntityFactory Instance;

    public EntityFactory()
    {
        Instance = this;
        EntityObjects = new();
    }

    public void AddEntityObject(EntityObject entityObject)
    {
        EntityObjects.Add(entityObject);
    }

    public void CreateEntityObjects()
    {
        foreach (EntityObject entityObject in EntityObjects)
        {
            if (EntityDictionary.ContainsKey(entityObject.AnimalType))
            {
                break;
            }

            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (var i = 0; i < entityObject.Number; i++)
            {
                GameObject obj = Instantiate(entityObject.Prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            EntityDictionary.Add(entityObject.AnimalType, objectQueue);
        }
    }

    public GameObject GetEntity(Animal animal)
    {
        if (!EntityDictionary.ContainsKey(animal.AnimalType))
        {
            Debug.LogWarning("No such animal: " + animal.AnimalType);
            return null;
        }

        GameObject entityObject = EntityDictionary[animal.AnimalType].Dequeue();
        entityObject.SetActive(true);

        float xPosition = animal.Position.X;
        float yPosition = animal.Position.Y;
        entityObject.transform.localPosition =
            new Vector3(xPosition, yPosition);

        return entityObject;
    }

    public void ReturnEntity(GameObject entityObject, AnimalType animal)
    {
        entityObject.SetActive(false);
        EntityDictionary[animal].Enqueue(entityObject);
    }

    public static int GetAnimalsNumber(AnimalType animalType)
    {
        int number = 0;
        switch (animalType)
        {
            case AnimalType.Rabbit:
                number = AnimalsNumber["Rabbits"];
                break;
            case AnimalType.Deer:
                number = AnimalsNumber["Deers"] * 10;
                break;
            case AnimalType.Wolf:
                number = AnimalsNumber["Wolves"];
                break;

        }
        return number;
    }
}
