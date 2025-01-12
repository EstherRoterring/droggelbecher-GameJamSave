using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public GlobalSceneManager()
    {
        INSTANCE = this;
    }

    public static GlobalSceneManager INSTANCE;

    public ScaleWobbleFrame transitionFrameP1;
    public ScaleWobbleFrame transitionFrameP2;

    private int currentSceneIndex = 0;

    private float sceneChangeTimer = 0.0f;
    private int nextSceneIndex = 0;

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

    public void TransitionToNextScene()
    {
        TransitionToScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
		SceneManager.LoadScene(1);
	}

    public void TransitionToScene(int sceneIndex, float timeBeforeSceneChange = 3.0f)
    {
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
		SceneManager.LoadScene(nextSceneIndex);
        currentSceneIndex = nextSceneIndex;
        Debug.Log("Playing Level Opening Animation");
		transitionFrameP1.PlayOpeningAnimation();
        transitionFrameP2.PlayOpeningAnimation();
	}
}
