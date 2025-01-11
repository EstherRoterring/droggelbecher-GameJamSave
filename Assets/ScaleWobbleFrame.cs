using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWobbleFrame : MonoBehaviour
{
    public float global_scale;
    // Start is called before the first frame update
    void Start()
    {
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
            child.transform.localScale = global_scale * new Vector3(1,1,1);

    }
}
