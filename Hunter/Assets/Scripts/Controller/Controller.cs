using UnityEngine;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using System;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    private HunterGame _game;
    private View _view;

    [Header("Game Settings")]

    [Header("Rabbits")]
    [SerializeField]
    private GameObject _rabbitsPrefab;

    [Header("Deers")]
    [SerializeField]
    private GameObject _deersPrefab;

    [Header("Wolves")]
    [SerializeField]
    private GameObject _wolvesPrefab;

    [Header("Hunter")]
    [SerializeField]
    private GameObject _hunterPrefab;

    private void Start()
    {
        int _rabbitsNumber = EntityFactory.GetAnimalsNumber(AnimalType.Rabbit);
        int _deersNumber = EntityFactory.GetAnimalsNumber(AnimalType.Deer) / 10;
        int _wolvesNumber = EntityFactory.GetAnimalsNumber(AnimalType.Wolf);

        _game = new(_rabbitsNumber, _deersNumber, _wolvesNumber);
        _view = gameObject.AddComponent<View>();
        
        _view.CreateHunter(_game.Hunter, _hunterPrefab);
        CreateAnimals();
    }

    private void Update()
    {
        ReadMoves();
        _game.Update();
        _view.ChangeGameObjectsPositions();
    }

    private void CreateAnimals()
    {
        _view.CreateEntityObjects(_rabbitsPrefab, _deersPrefab, _wolvesPrefab);
        foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
        {
            List<Entity> animals = _game.GetAnimals(animalType);
            _view.CreateEntities(animalType, animals);
        }
    }

    private void HunterControler()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Debug.Log(h + " " + v);
        _game.Hunter.MoveTo(h, v);
    }

    private void MousePosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 vectorEnd = Camera.main.ScreenToWorldPoint(screenPosition);
            //Debug.Log(worldPosition.x + " " + worldPosition.y + " " + worldPosition.z);
            vectorEnd.z = 0;
            Vector3 vectorStart = new Vector3(_game.Hunter.Position.X, _game.Hunter.Position.Y);
            if (_game.Hunter.MakeShot())
            {
                Debug.Log("Make Shot");
                var direction = (vectorEnd - vectorStart).normalized;
                vectorEnd = vectorStart + direction * _game.Hunter.ShotDistance;
                _view.DrawShotLine(vectorStart, vectorEnd);
            }
        }
    }

    private void ReadMoves()
    {
        HunterControler();
        MousePosition();
    }

}
