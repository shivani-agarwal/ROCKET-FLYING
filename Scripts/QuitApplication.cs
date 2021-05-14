using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            print(" We pushed Escape");
            Application.Quit();
        }
    }
}
