using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject[] buildings;
    public Vector2[] buildingCameraShifts;

    private bool initialized;
    private Vector3 playerOrigin;

    public Transform myCamera;
    
    void Update()
    {
        if (!initialized)
        {
            initialized = true;
            playerOrigin = myCamera.transform.position;
        }
        
        // compute perspective w.r.t. player (player == this)
        var playerOffset = myCamera.transform.position - playerOrigin;

        for (var i = 0; i < buildings.Length; i++)
        {
            var buildingShift = buildingCameraShifts[i];
            buildings[i].transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, 0)
                + new Vector3(playerOffset.x * buildingShift.x, playerOffset.y * buildingShift.y, buildings[i].transform.position.z);
        }
    }
}
