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
    private Sprite _rabbitsSprite;
    [SerializeField]
    private Color _rabbitsColor;
    [SerializeField]
    [TextArea]
    private string _rabbitsName;
    [SerializeField]
    [TextArea]
    private string _rabbitsParentName;
    [SerializeField]
    private Vector3 _rabbitsSize;

    private void Start()
    {
        _game = new(_rabbitsNumber, 0, 0);

        CreateEntities();
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
            GameObject entityObject = new GameObject();
            entityObject.transform.parent = rabbitsParent.transform;

            SpriteRenderer entitySpriteRenderer =
                entityObject.AddComponent<SpriteRenderer>();
            entitySpriteRenderer.sprite = _rabbitsSprite;
            entitySpriteRenderer.color = _rabbitsColor;

            float xPosition = entity.Position.X;
            float yPosition = entity.Position.Y;
            entityObject.transform.localPosition =
                new Vector3(xPosition, yPosition);

            entityObject.name = _rabbitsName;
            entityObject.transform.localScale = _rabbitsSize;

            _entities.Add(entity, entityObject);
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
