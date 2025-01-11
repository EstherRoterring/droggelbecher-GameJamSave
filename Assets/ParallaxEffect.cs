using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject[] buildings;
    public Vector2[] buildingCameraShifts;

    private bool initialized;
    private Vector3 playerOrigin;

    public Transform camera;
    
    void Update()
    {
        if (!initialized)
        {
            initialized = true;
            playerOrigin = this.transform.position;
        }
        
        // compute perspective w.r.t. player (player == this)
        var playerOffset = this.transform.position - playerOrigin;

        for (var i = 0; i < buildings.Length; i++)
        {
            var buildingShift = buildingCameraShifts[i];
            buildings[i].transform.position = camera.transform.position + 
                new Vector3(playerOffset.x * buildingShift.x, playerOffset.y * buildingShift.y, buildings[i].transform.position.z);
        }
    }
}
