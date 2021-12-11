using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class EntityObject
{
    public EntityType AnimalType;
    public GameObject Prefab;
    public int Number;

    public EntityObject(EntityType animal, GameObject prefab)
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
    public Dictionary<EntityType, Queue<GameObject>> EntityDictionary = new();

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
            Debug.Log(entityObject.AnimalType + " " + entityObject.Number);
            EntityDictionary.Add(entityObject.AnimalType, objectQueue);
        }
    }

    public GameObject GetEntity(Entity animal)
    {
        if (!EntityDictionary.ContainsKey(animal.EntityType))
        {
            Debug.LogWarning("No such animal: " + animal.EntityType);
            return null;
        }

        GameObject entityObject = EntityDictionary[animal.EntityType].Dequeue();
        entityObject.SetActive(true);

        float xPosition = animal.Position.X;
        float yPosition = animal.Position.Y;
        entityObject.transform.localPosition =
            new Vector3(xPosition, yPosition);

        return entityObject;
    }

    public void ReturnEntity(GameObject entityObject, EntityType animal)
    {
        entityObject.SetActive(false);
        EntityDictionary[animal].Enqueue(entityObject);
    }

    public static int GetAnimalsNumber(EntityType animalType)
    {
        int number = 0;
        switch (animalType)
        {
            case EntityType.Rabbit:
                number = AnimalsNumber["Rabbits"];
                break;
            case EntityType.Deer:
                number = AnimalsNumber["Deers"] * 10;
                break;
            case EntityType.Wolf:
                number = AnimalsNumber["Wolves"];
                break;
            case EntityType.Hunter:
                number = 1;
                break;
        }
        return number;
    }
}
