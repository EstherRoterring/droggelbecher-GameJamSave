using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float currentTime = 0.0f;

    void Update()
    {
        currentTime += Time.deltaTime;
    }
}
