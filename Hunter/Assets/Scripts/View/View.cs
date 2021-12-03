using System.Collections.Generic;
using Hunter.Model;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;
using UnityEngine;
using System;

public class View : MonoBehaviour
{
    private HunterGame _game;
    private readonly Dictionary<Entity, GameObject> _entities = new();
    private EntityFactory _entityFactory;

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

    private void Awake()
    {
        _entityFactory = new EntityFactory();

    }

    private void Start()
    {
        _game = new(_rabbitsNumber, _deersNumber, 0);

        CreateEntityObjects();
        CreateEntities();
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
            //List<Animal> listOfAnimals = _game.Animals[animalType];
            List<Animal> listOfAnimals = GetListOfAnimals(animalType);
            foreach (Animal anim in listOfAnimals)
            {
                GameObject entityObject = _entityFactory.GetEntity(animalType, anim);

                _entities.Add(anim, entityObject);
            }
        }
    }

    private List<Animal> GetListOfAnimals(AnimalType animalType)
    {
        List<Animal> listOfAnimals = new();
        switch (animalType)
        {
            case AnimalType.Rabbit:
                listOfAnimals = _game.Animals[animalType];
                break;
            case AnimalType.Deer:
                foreach (Herd herd in _game.Animals[animalType])
                {
                    foreach (Animal anim in herd.GetAnimals())
                    {
                        listOfAnimals.Add(anim);
                    }
                }
                break;
        }
        return listOfAnimals;
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
