using System.Collections.Generic;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;

public class View : MonoBehaviour
{
    private HunterGame _game;
    private Dictionary<Entity, GameObject> _entities = new();

    [Header("Game Settings")]

    [Header("Rabbits")]
    [Range(0, 50)]
    [SerializeField]
    private int _rabbitsNumber;
    [SerializeField]
    private GameObject _rabbitsPrefab;
    [SerializeField]
    [TextArea]
    private string _rabbitsParentName;

    [Header("Deers")]
    [Range(0, 10)]
    [SerializeField]
    private int _deersNumber;
    [SerializeField]
    private GameObject _deersPrefab;
    [SerializeField]
    [TextArea]
    private string _deersParentName;

    private void Start()
    {
        _game = new(_rabbitsNumber, _deersNumber, 0);

        CreateEntities();
        CreateEntitiesDeers();
    }

    // TO-DO: use some generic type to create all entities using this
    // Method (maybe use some enums)
    private void CreateEntities()
    {
        GameObject rabbitsParent = new GameObject
        {
            name = _rabbitsParentName
        };
        rabbitsParent.transform.parent = transform;

        foreach (Entity entity in _game.Rabbits)
        {
            GameObject entityObject = Instantiate(_rabbitsPrefab,
                rabbitsParent.transform);

            float xPosition = entity.Position.X;
            float yPosition = entity.Position.Y;
            entityObject.transform.localPosition =
                new Vector3(xPosition, yPosition);

            _entities.Add(entity, entityObject);
        }
    }

    private void CreateEntitiesDeers()
    {
        GameObject deersParent = new GameObject
        {
            name = _deersParentName
        };
        deersParent.transform.parent = transform;

        foreach (Herd herd in _game.HerdsOfDeer)
        {
            foreach (Entity entity in herd.Deers)
            {
                GameObject entityObject = Instantiate(_deersPrefab,
                deersParent.transform);

                float xPosition = entity.Position.X;
                float yPosition = entity.Position.Y;
                entityObject.transform.localPosition =
                    new Vector3(xPosition, yPosition);

                _entities.Add(entity, entityObject);
            }

        }
    }

    private void Update()
    {
        _game.Update();

        ChangeGameObjectsPositions();
    }

    private void ChangeGameObjectsPositions()
    {
        foreach (KeyValuePair<Entity, GameObject> keyValue in _entities)
        {
            float xPos = keyValue.Key.Position.X;
            float yPos = keyValue.Key.Position.Y;

            Vector3 newPosition = new Vector3(xPos, yPos);
            keyValue.Value.transform.localPosition = newPosition;
        }
    }
}
