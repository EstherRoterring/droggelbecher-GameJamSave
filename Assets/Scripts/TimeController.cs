using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float currentTime = 0.0f;

    private List<TimeControlledObject> registeredObjects = new List<TimeControlledObject>();

    void Update()
    {
        currentTime += Time.deltaTime;
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
