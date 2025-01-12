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
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            someoneDiedController.someoneDied = true;
            GlobalSceneManager.INSTANCE.ScheduleTransitionToScene(1);
        }
    }
}
