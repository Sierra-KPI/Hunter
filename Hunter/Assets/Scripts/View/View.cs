using System.Collections.Generic;
using Hunter.Model.Entities;
using UnityEngine;

public class View : MonoBehaviour
{
    private readonly Dictionary<Entity, GameObject> _entities = new();
    private EntityFactory _entityFactory = new();

    public Material LineMaterial;

    public void CreateHunter(HunterPlayer hunter)
    {
        GameObject obj = _entityFactory.GetEntity(hunter);
        _entities.Add(hunter, obj);
    }

    public void CreateEntityObjects(GameObject _rabbitsPrefab, GameObject _deersPrefab,
        GameObject _wolvesPrefab, GameObject _hunterPrefab)
    {
        var _rabbitObject = new EntityObject(
            EntityType.Rabbit,
            _rabbitsPrefab
        );
        _entityFactory.AddEntityObject(_rabbitObject);

        var _deerObject = new EntityObject(
            EntityType.Deer,
            _deersPrefab
        );
        _entityFactory.AddEntityObject(_deerObject);

        var _wolfObject = new EntityObject(
            EntityType.Wolf,
            _wolvesPrefab
        );
        _entityFactory.AddEntityObject(_wolfObject);

        var _hunterObject = new EntityObject(
            EntityType.Hunter,
            _hunterPrefab
        );
        _entityFactory.AddEntityObject(_hunterObject);

        _entityFactory.CreateEntityObjects();
    }

    public void CreateEntities(List<Entity> Entities)
    {
        foreach (Entity entity in Entities)
        {
            GameObject entityObject = _entityFactory.GetEntity(entity);
            _entities.Add(entity, entityObject);
        }
    }

    public void DestroyEntity(Entity entity)
    {
        var obj = _entities.GetValueOrDefault(entity);
        _entityFactory.ReturnEntity(obj, entity.EntityType);
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
                DestroyEntity(keyValue.Key);
            }
        }
    }
}
