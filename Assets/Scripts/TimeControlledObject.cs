using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class TimeControlledObject : MonoBehaviour
{
    TimeController timeController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = 100f * new Vector3(Mathf.Cos(timeController.currentTime), Mathf.Sin(timeController.currentTime));
    }
}
