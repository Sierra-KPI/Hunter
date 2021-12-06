using UnityEngine;

public class Controller : MonoBehaviour
{

    private void HunterControler()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log(h + " " + v);
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
