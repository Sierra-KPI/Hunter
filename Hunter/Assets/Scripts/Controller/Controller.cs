using UnityEngine;

public class Controller : MonoBehaviour
{

    private void HunterControler()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Hunter Move Forward");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Hunter Move Back");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Hunter Move Left");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Hunter Move Right");
        }
    }

    private void MousePosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            Debug.Log(worldPosition.x + " " + worldPosition.y);
        }
    }

    public void ReadMoves()
    {
        HunterControler();
        MousePosition();
    }

}
