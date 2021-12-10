using UnityEngine;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using System;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    private HunterGame _game;
    private View _view;
    private SceneLoader _sceneLoader;

    [Header("Game Settings")]

    [Header("Rabbits")]
    [SerializeField]
    private GameObject _rabbitsPrefab;

    [Header("Deers")]
    [SerializeField]
    private GameObject _deersPrefab;

    [Header("Hunter")]
    [SerializeField]
    private GameObject _hunterPrefab;

    [SerializeField]
    private Material _lineMaterial;

    private void Start()
    {
        int _rabbitsNumber = EntityFactory.GetAnimalsNumber(AnimalType.Rabbit);
        int _deersNumber = EntityFactory.GetAnimalsNumber(AnimalType.Deer) / 10;
        //int _wolvesNumber = EntityFactory.GetAnimalsNumber(AnimalType.Wolf);

        _game = new(_rabbitsNumber, _deersNumber, 0);
        _view = gameObject.AddComponent<View>();
        _view.LineMaterial = _lineMaterial;

        _sceneLoader = gameObject.AddComponent<SceneLoader>();
        _sceneLoader.SetPauseMenu();
        
        _view.CreateHunter(_game.Hunter, _hunterPrefab);
        CreateAnimals();
    }

    private void Update()
    {
        PauseMenuController();
        if (_sceneLoader.isPaused) return;
        ReadMoves();
        _game.Update();
        _view.ChangeGameObjectsPositions();
        _view.DeleteDeadAnimals();

        //CheckGameEnd();

    }

    private void CreateAnimals()
    {
        _view.CreateEntityObjects(_rabbitsPrefab, _deersPrefab);
        foreach (AnimalType animalType in
            (AnimalType[])Enum.GetValues(typeof(AnimalType)))
        {
            List<Entity> animals = _game.GetAnimals(animalType);
            _view.CreateEntities(animals);
        }
    }

    private void HunterControler()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _game.Hunter.MoveTo(h, v);
    }

    private void MousePosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y);
            Vector3 vectorEnd = Camera.main.ScreenToWorldPoint(screenPosition);
            vectorEnd.z = 0;
            Vector3 vectorStart = new Vector3(_game.Hunter.Position.X,
                _game.Hunter.Position.Y);

            if (_game.Hunter.MakeShot())
            {
                Debug.Log("Make Shot");
                var deadAnimal = _game.TryToKillAnimalByShot(vectorEnd.x,
                    vectorEnd.y);

                var shotLength = _game.Hunter.ShotDistance;

                if (deadAnimal != null)
                {
                    _view.DestroyEntity(deadAnimal);
                    shotLength = (_game.Hunter.Position -
                        deadAnimal.Position).Length();
                }

                var direction = (vectorEnd - vectorStart).normalized;
                vectorEnd = vectorStart + direction * shotLength;
                _view.DrawShotLine(vectorStart, vectorEnd);
            }
        }
    }

    private void ReadMoves()
    {
        HunterControler();
        MousePosition();
    }

    private void PauseMenuController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sceneLoader.LoadPauseMenu();
            
        }
    }

    private void CheckGameEnd()
    {
        if (_game.GetAllEntities().Count == 0)
        {
            _sceneLoader.LoadWinningGameEnd();
        }
    }
}
