using System.Collections.Generic;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;

public class View : MonoBehaviour
{
    private HunterGame _game;
    private readonly Dictionary<Entity, GameObject> _entities = new();
    [SerializeField]
    private Sprite _rabbitSprite;

    private void Start()
    {
        _game = new(1, 0, 0);

        foreach (Rabbit rabbit in _game.Rabbits)
        {
            GameObject rabbitObject = new GameObject();
            SpriteRenderer rabbitSpriteRenderer =
                rabbitObject.AddComponent<SpriteRenderer>();
            rabbitSpriteRenderer.sprite = _rabbitSprite;
            rabbitSpriteRenderer.color = Color.blue;
            float xPosition = rabbit.Position.X;
            float yPosition = rabbit.Position.Y;
            rabbitObject.transform.localPosition =
                new Vector3(xPosition, yPosition);
            rabbitObject.name = "Rabbit";
            rabbitObject.transform.localScale = new Vector3(3,3);
            _entities.Add(rabbit, rabbitObject);
        }
    }

    private void Update()
    {
        _game.Update();

        foreach (KeyValuePair<Entity, GameObject> keyValue in _entities)
        {
            float xPos = keyValue.Key.Position.X;
            float yPos = keyValue.Key.Position.Y;
            Vector3 newPosition = new Vector3(xPos, yPos);
            keyValue.Value.transform.localPosition = newPosition;
        }
    }
}
