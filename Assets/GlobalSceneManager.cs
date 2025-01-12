using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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

    public float sceneChangeTimer = 0.0f;
    private int nextSceneIndex = 0;
    private bool transitionQueued = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = 0;
		sceneChangeTimer = 0;
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
                sceneChangeTimer = 0;
                DoTransition();
			}
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

    public void TransitionToNextScene(float timeBeforeSceneChange = 3.0f)
    {
        TransitionToScene(currentSceneIndex + 1, timeBeforeSceneChange, "next level!");
    }

    public void LoadMainMenu()
    {
		SceneManager.LoadScene(1);
	}

    public void ReloadScene(float timeBeforeSceneChange = 3.0f, string msg = "nix")
    {
        TransitionToScene(currentSceneIndex, timeBeforeSceneChange, msg);
    }

    public void TransitionToScene(int sceneIndex, float timeBeforeSceneChange = 3.0f, string msg = "nix")
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
		sceneChangeTimer = 0;
		currentSceneIndex = nextSceneIndex;
        Debug.Log("Playing Level Opening Animation");
		transitionFrameP1.PlayOpeningAnimation();
        transitionFrameP2.PlayOpeningAnimation();
        SceneManager.LoadScene(nextSceneIndex);
		sceneChangeTimer = 0;
        transitionQueued = false;
	}
}
