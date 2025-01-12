using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public Collider2D player01;
    public Collider2D player02;

    public static bool playerOneInGoal;
    public static bool playerTwoInGoal;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player1 :" + playerOneInGoal + "Player2:" + playerTwoInGoal);

        if(playerOneInGoal && playerTwoInGoal){
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        // Aktuelle Szene holen
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            GlobalSceneManager.INSTANCE.TransitionToNextScene();
        }
        else
        {
            Debug.Log("Keine weitere Szene vorhanden!");
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Something goaled");
        //Player kommen ins Ziel
        if(other == player01 && !playerOneInGoal)
        {
            GlobalSceneManager.INSTANCE.transitionFrameP1.PlayClosingAnimation();
			playerOneInGoal = true;
        }
        if(other == player02 && !playerTwoInGoal)
        {
			GlobalSceneManager.INSTANCE.transitionFrameP2.PlayClosingAnimation();
			playerTwoInGoal = true;
        }
    
    }
}
