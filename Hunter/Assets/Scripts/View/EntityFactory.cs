using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class EntityObject
{
    public AnimalType AnimalType;
    public GameObject Prefab;
    public int Number;

    public EntityObject(AnimalType animal, GameObject prefab, int number)
    {
        AnimalType = animal;
        Prefab = prefab;
        Number = number;
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

    public GameObject GetEntity(AnimalType animal, Entity entity)
    {
        if (!EntityDictionary.ContainsKey(animal))
        {
            Debug.LogWarning("No such animal: " + animal);
            return null;
        }

        GameObject entityObject = EntityDictionary[animal].Dequeue();
        entityObject.SetActive(true);

        float xPosition = entity.Position.X;
        float yPosition = entity.Position.Y;
        entityObject.transform.localPosition =
            new Vector3(xPosition, yPosition);

        return entityObject;
    }

    public void ReturnEntity(GameObject entityObject, AnimalType animal)
    {
        entityObject.SetActive(false);
        EntityDictionary[animal].Enqueue(entityObject);
    }
}
