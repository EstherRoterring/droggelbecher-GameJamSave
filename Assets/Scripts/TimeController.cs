using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float currentTime = 0.0f;

    public float autoTimeScale;
    public float controllingObjectDistanceScale;
    public PlayerController controllingObject;
    public float minControllingTimeChange = 0.0001f;

    public bool isTimeChanging = false;
    
    private bool controllingObjectPositionInitialized = false;
    private Vector3 oldControllingObjectPosition = Vector3.zero;
    public float lastTimeMoved;

    private List<TimeControlledObject> registeredObjects = new List<TimeControlledObject>();

    void Update()
    {
        var oldCurrentTime = currentTime;
        
        currentTime += this.autoTimeScale * Time.deltaTime;

        if (controllingObjectPositionInitialized)
        {
            var positionChange = (controllingObject.transform.position - oldControllingObjectPosition).magnitude;
            if (positionChange > 0.0000000001f || !controllingObject.groundIsTouched)
            {
                lastTimeMoved = Time.time;
            }
            if (lastTimeMoved >= Time.time - minControllingTimeChange)
            {
                currentTime += this.controllingObjectDistanceScale * Time.deltaTime;
            }
        }
        else
        {
            controllingObjectPositionInitialized = true;
        }
        oldControllingObjectPosition = controllingObject.transform.position;

        isTimeChanging = (currentTime > oldCurrentTime);
    }

    public void TimeRegisteredUpdate(float deltaTime)
    {
        // don't really need this anymore...
        // foreach (var obj in registeredObjects)
        // {
        //     obj.TimeControlledUpdate(Time.deltaTime);
        // }
    }

    public void RegisterObject(TimeControlledObject obj)
    {
        this.registeredObjects.Add(obj);
    }

    public void DeregisterObject(TimeControlledObject obj)
    {
        this.registeredObjects.Remove(obj);
    }
}
