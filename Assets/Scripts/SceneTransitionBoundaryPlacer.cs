using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionBoundaryPlacer : MonoBehaviour
{
    public BoxCollider2D boundaryBox;
    public int playerID = 0;
    private ScaleWobbleFrame myWobbleFrame;

    // Start is called before the first frame update
    void Start()
    {
        myWobbleFrame = playerID == 0 ? GlobalSceneManager.INSTANCE.transitionFrameP1 : GlobalSceneManager.INSTANCE.transitionFrameP2;

        myWobbleFrame.transform.position = transform.position;

        // change top and bottom fuuuuuuuuck
    }
}
