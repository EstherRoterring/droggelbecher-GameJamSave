using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public GlobalSceneManager()
    {
        Debug.Log("Constructed a new scene manager");
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
    }

    public static GlobalSceneManager INSTANCE;

    public ScaleWobbleFrame transitionFrameP1;
    public ScaleWobbleFrame transitionFrameP2;

    private int currentSceneIndex = 0;

    public float sceneChangeTimer = -1;
    private int nextSceneIndex = 0;
    private bool transitionQueued = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = 0;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneChangeTimer > 0)
        {
            sceneChangeTimer -= Time.deltaTime;
            if (sceneChangeTimer < 0)
            {
                DoTransition();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    public int GetCurrentSceneIndex()
    {
        return currentSceneIndex;
    }

    public bool IsTransitioningBetweenScenes()
    {
        return sceneChangeTimer > 0.0f;
    }

    public void ScheduleTransitionToNextScene(float timeBeforeSceneChange = 1.0f)
    {
        ScheduleTransitionToScene(currentSceneIndex + 1, timeBeforeSceneChange, "next level!");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ScheduleReloadScene(float timeBeforeSceneChange = 1.0f, string msg = "nix")
    {
        ScheduleTransitionToScene(currentSceneIndex, timeBeforeSceneChange, msg);
    }

    public void ScheduleTransitionToScene(int sceneIndex, float timeBeforeSceneChange = 1.0f, string msg = "nix")
    {
        Debug.Log("TRANSITION QUEUED! " + msg);

        if (transitionQueued)
        {
            Debug.Log("Transition Cancelled!");
            return;
        }
        transitionQueued = true;

        if (!transitionFrameP1.FrameClosingOrClosed())
        {
            transitionFrameP1.PlayClosingAnimation();
        }
        if (!transitionFrameP2.FrameClosingOrClosed())
        {
            transitionFrameP2.PlayClosingAnimation();
        }

        sceneChangeTimer = timeBeforeSceneChange;
        nextSceneIndex = sceneIndex;
    }

    private void DoTransition()
    {
        sceneChangeTimer = -1;
        currentSceneIndex = nextSceneIndex;
        Debug.Log("Playing Level Opening Animation");
        SceneManager.LoadScene(nextSceneIndex);
        transitionFrameP1.PlayOpeningAnimation();
        transitionFrameP2.PlayOpeningAnimation();
        sceneChangeTimer = -1;
        transitionQueued = false;
    }
}
