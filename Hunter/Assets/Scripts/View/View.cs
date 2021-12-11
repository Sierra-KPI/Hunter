using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class View : MonoBehaviour
{
    private readonly Dictionary<Entity, GameObject> _entities = new();
    private EntityFactory _entityFactory = new();

    public Material LineMaterial;

    public void CreateHunter(HunterPlayer hunter, GameObject hunterPrefab)
    {
        GameObject obj = Instantiate(hunterPrefab);
        _entities.Add(hunter, obj);
    }

    public void CreateEntityObjects(GameObject _rabbitsPrefab, GameObject _deersPrefab, GameObject _wolvesPrefab)
    {
        var _rabbitObject = new EntityObject(
            AnimalType.Rabbit,
            _rabbitsPrefab
        );
        _entityFactory.AddEntityObject(_rabbitObject);

        var _deerObject = new EntityObject(
            AnimalType.Deer,
            _deersPrefab
        );
        _entityFactory.AddEntityObject(_deerObject);

        var _wolfObject = new EntityObject(
            AnimalType.Wolf,
            _wolvesPrefab
        );
        _entityFactory.AddEntityObject(_wolfObject);

        _entityFactory.CreateEntityObjects();
    }

    public void CreateEntities(List<Entity> Entities)
    {
        foreach (Animal animal in Entities)
        {
            GameObject entityObject = _entityFactory.GetEntity(animal);
            _entities.Add(animal, entityObject);
        }
    }

    public void DestroyEntity(Animal animal)
    {
        var obj = _entities.GetValueOrDefault(animal);
        _entityFactory.ReturnEntity(obj, animal.AnimalType);
    }

    public void ChangeGameObjectsPositions()
    {
        foreach (KeyValuePair<Entity, GameObject> keyValue in _entities)
        {
            float xPos = keyValue.Key.Position.X;
            float yPos = keyValue.Key.Position.Y;

            Vector3 newPosition = new Vector3(xPos, yPos);
            keyValue.Value.transform.localPosition = newPosition;
        }
    }

    public void DrawShotLine(Vector3 start, Vector3 end, float duration = 0.3f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer line = myLine.GetComponent<LineRenderer>();
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.material = LineMaterial;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.SetPositions(new Vector3[] { start, end });
        Destroy(myLine, duration);
    }

    public void DeleteDeadAnimals()
    {
        foreach (KeyValuePair<Entity, GameObject> keyValue in _entities)
        {
            if (keyValue.Key.IsDead)
            {
                DestroyEntity((Animal)keyValue.Key);
            }
        }
    }
}
