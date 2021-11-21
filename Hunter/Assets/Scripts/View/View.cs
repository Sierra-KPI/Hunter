using System.Collections.Generic;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;

public class View : MonoBehaviour
{
    private HunterGame _game;
    private List<GameObject> _entities;
    [SerializeField]
    private Sprite rabbitSprite;

    private void Start()
    {
        _game = new(1, 0, 0);

        foreach (Rabbit rabbit in _game.Rabbits)
        {
            GameObject rabbitObject = new GameObject();
            SpriteRenderer rabbitSpriteRenderer =
                rabbitObject.AddComponent<SpriteRenderer>();
            rabbitSpriteRenderer.sprite = rabbitSprite;
            rabbitSpriteRenderer.color = Color.blue;
            rabbitObject.transform.localPosition = Vector3.zero;
            rabbitObject.name = "Rabbit";
            //Instantiate(rabbitObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {

        
    }
}
