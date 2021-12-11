using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class EntityObject
{
    public EntityType EntityType;
    public GameObject Prefab;
    public int Number;

    public EntityObject(EntityType entityType, GameObject prefab)
    {
        EntityType = entityType;
        Prefab = prefab;
        Number = EntityFactory.GetEntitiesNumber(entityType);
    }
}

public class EntityFactory : MonoBehaviour
{
    public static Dictionary<string, int> EntitiesNumber = new();

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
            if (EntityDictionary.ContainsKey(entityObject.EntityType))
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
            EntityDictionary.Add(entityObject.EntityType, objectQueue);
        }
    }

    public GameObject GetEntity(Entity entity)
    {
        if (!EntityDictionary.ContainsKey(entity.EntityType))
        {
            Debug.LogWarning("No such entity: " + entity.EntityType);
            return null;
        }

        GameObject entityObject = EntityDictionary[entity.EntityType].Dequeue();
        entityObject.SetActive(true);

        float xPosition = entity.Position.X;
        float yPosition = entity.Position.Y;
        entityObject.transform.localPosition =
            new Vector3(xPosition, yPosition);

        return entityObject;
    }

    public void ReturnEntity(GameObject entityObject, EntityType entityType)
    {
        entityObject.SetActive(false);
        EntityDictionary[entityType].Enqueue(entityObject);
    }

    public static int GetEntitiesNumber(EntityType entityType)
    {
        int number = 0;
        switch (entityType)
        {
            case EntityType.Rabbit:
                number = EntitiesNumber["Rabbits"];
                break;
            case EntityType.Deer:
                number = EntitiesNumber["Deers"] * 10;
                break;
            case EntityType.Wolf:
                number = EntitiesNumber["Wolves"];
                break;
            case EntityType.Hunter:
                number = 1;
                break;
        }
        return number;
    }
}
