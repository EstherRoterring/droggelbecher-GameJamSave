using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public SomeoneDiedController someoneDiedController;
    
    void Update()
    {
        if (someoneDiedController.someoneDied)
        {
            return; // don't die again.
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            someoneDiedController.someoneDied = true;
            GlobalSceneManager.INSTANCE.ScheduleReloadScene();
        }
    }
}
