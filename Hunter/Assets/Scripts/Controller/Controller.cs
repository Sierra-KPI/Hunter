using UnityEngine;

public class Controller : MonoBehaviour
{

    public (float, float) HunterControler()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Debug.Log(h + " " + v);
        return (h, v);
    }

    public Vector3 MousePosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //Debug.Log(worldPosition.x + " " + worldPosition.y + " " + worldPosition.z);
            worldPosition.z = 0;
            return worldPosition;
        }
        return Vector3.zero;
    }

    public void ReadMoves()
    {
        HunterControler();
        MousePosition();
    }

}
