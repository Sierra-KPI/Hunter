using System;
using System.Collections.Generic;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;

public class View : MonoBehaviour
{

    private readonly Dictionary<Entity, GameObject> _entities = new();
    private EntityFactory _entityFactory = new();

    [Header("Game Settings")]

    [Header("Rabbits")]
    [SerializeField]
    private GameObject _rabbitsPrefab;
    private int _rabbitsNumber;

    [Header("Deers")]
    [SerializeField]
    private GameObject _deersPrefab;
    private int _deersNumber;

    [Header("Wolves")]
    private int _wolvesNumber;

    [Header("Hunter")]
    [SerializeField]
    private GameObject _hunterPrefab;


    public void CreateHunter(HunterPlayer hunter)
    {
        GameObject obj = Instantiate(_hunterPrefab);
        _entities.Add(hunter, obj);
    }

    private void CreateEntityObjects()
    {
        var _rabbitObject = new EntityObject(
            AnimalType.Rabbit,
            _rabbitsPrefab,
            _rabbitsNumber
        );
        _entityFactory.AddEntityObject(_rabbitObject);

        var _deerObject = new EntityObject(
            AnimalType.Deer,
            _deersPrefab,
            _deersNumber * 10
        );
        _entityFactory.AddEntityObject(_deerObject);

        _entityFactory.CreateEntityObjects();
    }

    public void CreateEntities(Dictionary<AnimalType, List<Entity>> Entities)
    {
        CreateEntityObjects();
        foreach (KeyValuePair<AnimalType, List<Entity>> value in Entities)
        {
            List<Entity> animals = value.Value;
            foreach (Animal anim in animals)
            {
                GameObject entityObject = _entityFactory.GetEntity(value.Key, anim);

                _entities.Add(anim, entityObject);
            }
        }
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

    public void DrawShotLine(Vector3 start, Vector3 end, float duration = 0.5f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer line = myLine.GetComponent<LineRenderer>();
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.SetPositions(new Vector3[] { start, end });
        Destroy(myLine, duration);
    }

}
