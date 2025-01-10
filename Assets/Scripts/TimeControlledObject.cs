using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class TimeControlledObject : MonoBehaviour
{
    public TimeController timeController;
    public GameObject keyFrameContainer;
    public GameObject actualObject;

    private KeyFrame[] keyFrames;
    private float totalLoopTime;
    
    // Start is called before the first frame update
    void Start()
    {
        keyFrames = keyFrameContainer.GetComponentsInChildren<KeyFrame>();
        totalLoopTime = 0.0f;
        foreach (var keyFrame in keyFrames)
        {
            totalLoopTime += keyFrame.keyFrameDuration;
        }
    }

    private void OnEnable()
    {
        timeController.RegisterObject(this);
    }

    private void OnDisable()
    {
        timeController.DeregisterObject(this);
    }

    // Update is called once per frame
    public void Update()
    {
        // find out in which keyframe we are.
        float t = timeController.currentTime % totalLoopTime;
        for (var k = 0; k < keyFrames.Length; k++)
        {
            var keyFrame = keyFrames[k];
            if (t > keyFrame.keyFrameDuration)
            {
                t -= keyFrame.keyFrameDuration;
            }
            else
            {
                var alpha = t / keyFrame.keyFrameDuration;
                // interpolate 
                var nextKeyFrame = keyFrames[(k + 1) % keyFrames.Length];

                actualObject.transform.position =
                    Vector3.Lerp(keyFrame.transform.position, nextKeyFrame.transform.position, alpha);
                actualObject.transform.localScale = (1 - alpha) * keyFrame.transform.localScale +
                                                    alpha * nextKeyFrame.transform.localScale;
                actualObject.transform.rotation = Quaternion.Lerp(keyFrame.transform.rotation, nextKeyFrame.transform.rotation, alpha);
                break;
            }
        }
    }
}
