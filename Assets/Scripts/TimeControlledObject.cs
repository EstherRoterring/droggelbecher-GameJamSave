using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.TestTools;

public class TimeControlledObject : MonoBehaviour
{
    public TimeController timeController;
    public GameObject keyFrameContainer;
    public GameObject actualObject;

    public bool easing;
    public bool randomOffset;

    private KeyFrame[] keyFrames;
    private float totalLoopTime;

    private float timeOffset;
    private float noiseScale;

    public List<Transform> busPassengers = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        keyFrames = keyFrameContainer.GetComponentsInChildren<KeyFrame>();
        totalLoopTime = 0.0f;
        foreach (var keyFrame in keyFrames)
        {
            totalLoopTime += keyFrame.keyFrameDuration;
        }

        if (randomOffset)
        {
            timeOffset = Random.Range(0, 100f);
        }
        else
        {
            timeOffset = 0f;
        }

        noiseScale = Random.Range(0.5f, 1.5f);
    }

    private void OnEnable()
    {
        timeController.RegisterObject(this);
    }

    private void OnDisable()
    {
        timeController.DeregisterObject(this);
    }

    public float EasingFunction(float x)
    {
        if (easing) {
            // return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }
        else
        {
            return x;
        }
    }

    public float FourierNoise(float t)
    {
        t *= noiseScale;
        return Mathf.Sin(0.4f * t) + 0.3f * Mathf.Sin(1f * t) + 0.1f * Mathf.Sin(2.5f * t);
    }
    
    // Update is called once per frame
    public void Update()
    {
        var lastPosition = actualObject.transform.position;
        
        var currentTime = timeController.currentTime + timeOffset;
        // find out in which keyframe we are.
        float t = currentTime % totalLoopTime;
        for (var k = 0; k < keyFrames.Length; k++)
        {
            var keyFrame = keyFrames[k];
            if (t > keyFrame.keyFrameDuration)
            {
                t -= keyFrame.keyFrameDuration;
            }
            else
            {
                var alpha = EasingFunction(t / keyFrame.keyFrameDuration);
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
        // add local noise, but how?
        actualObject.transform.localScale *= (1f + FourierNoise(2f * currentTime) * 0.2f);
        actualObject.transform.Rotate(Vector3.back, FourierNoise(5f * currentTime + 123f));

        foreach (var player in this.busPassengers)
        {
            player.transform.position = player.transform.position + actualObject.transform.position - lastPosition;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);

        var debugKeyFrames = keyFrameContainer.GetComponentsInChildren<KeyFrame>();
        for (var k = 0; k < debugKeyFrames.Length; k++)
        {
            var keyFrame = debugKeyFrames[k];
            var nextKeyFrame = debugKeyFrames[(k + 1) % debugKeyFrames.Length];
            Gizmos.DrawLine(keyFrame.transform.position, nextKeyFrame.transform.position);
        }
    }
}
