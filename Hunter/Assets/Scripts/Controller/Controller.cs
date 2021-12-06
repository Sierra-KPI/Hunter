using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public void Update()
    {

        HunterControler();
    }


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

}
