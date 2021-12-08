using UnityEngine;
using Hunter.Model.Entities;
using Hunter.Model.HunterGame;

public class Controller : MonoBehaviour
{

    private HunterGame _game;
    private View _view;



    private void Start()
    {
        int _rabbitsNumber = EntityFactory.AnimalsNumber["Rabbits"];
        int _deersNumber = EntityFactory.AnimalsNumber["Deers"];
        int _wolvesNumber = EntityFactory.AnimalsNumber["Wolves"];

        _game = new(_rabbitsNumber, _deersNumber, _wolvesNumber);
        _view = gameObject.AddComponent<View>();
        
        _view.CreateHunter(_game.Hunter);
        _view.CreateEntities(_game.Entities);
    }

    private void Update()
    {
        ReadMoves();
        _game.Update();
        _view.ChangeGameObjectsPositions();
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
