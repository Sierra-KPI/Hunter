using System;
using System.Collections.Generic;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;

public class View : MonoBehaviour
{
    private Controller _controller;
    private HunterGame _game;
    private readonly Dictionary<Entity, GameObject> _entities = new();
    private EntityFactory _entityFactory;

    [Header("Game Settings")]

    [Header("Rabbits")]
    private int _rabbitsNumber;
    [SerializeField]
    private GameObject _rabbitsPrefab;
    [SerializeField]
    [TextArea]
    private string _rabbitsParentName;

    [Header("Deers")]
    private int _deersNumber;
    [SerializeField]
    private GameObject _deersPrefab;
    [SerializeField]
    [TextArea]
    private string _deersParentName;

    [Header("Wolves")]
    private int _wolvesNumber;

    [Header("Hunter")]
    [SerializeField]
    private GameObject _hunterPrefab;

    private void Awake()
    {
        _entityFactory = new EntityFactory();
    }

    private void Start()
    {
        _rabbitsNumber = EntityFactory.AnimalsNumber["Rabbits"];
        _deersNumber = EntityFactory.AnimalsNumber["Deers"];
        _wolvesNumber = EntityFactory.AnimalsNumber["Wolves"];

        _game = new(_rabbitsNumber, _deersNumber, _wolvesNumber);
        _controller = new Controller();

        CreateEntityObjects();
        CreateEntities();
        CreateHunter();
    }

    private void CreateHunter()
    {
        GameObject obj = Instantiate(_hunterPrefab);
        _entities.Add(_game.Hunter, obj);
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

    private void CreateEntities()
    {
        foreach (AnimalType animalType in (AnimalType[])Enum.GetValues(typeof(AnimalType)))
        {
            List<Entity> animals = _game.GetAnimals(animalType);
            foreach (Animal anim in animals)
            {
                GameObject entityObject = _entityFactory.GetEntity(animalType, anim);

                _entities.Add(anim, entityObject);
            }
        }
    }

    private void Update()
    {
        _game.Update();

        //_controller.ReadMoves();

        (float h, float v) = _controller.HunterControler();
        Vector3 vectorEnd = _controller.MousePosition();
        Vector3 vectorStart = new Vector3(_game.Hunter.Position.X, _game.Hunter.Position.Y); 
        if (vectorEnd != Vector3.zero && _game.Hunter.MakeShot())
        {
            Debug.Log("Make Shot");
            //vectorEnd = (vectorEnd - vectorStart).normalized * _game.Hunter.ShotDistance;
            DrawShotLine(vectorStart, vectorEnd);
        }
        _game.Hunter.MoveTo(h, v);

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

    private void DrawShotLine(Vector3 start, Vector3 end, float duration = 0.5f)
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
