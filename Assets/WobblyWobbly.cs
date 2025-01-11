using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyWobbly : MonoBehaviour
{
    
    private float timeOffset;
    private float noiseScale;
    
    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0, 100f);
        noiseScale = Random.Range(0.5f, 1.5f);
    }
    
    public float FourierNoise(float t)
    {
        // inspired by Anton
        t *= noiseScale;
        return Mathf.Sin(0.4f * t) + 0.3f * Mathf.Sin(1f * t + 3f) + 0.1f * Mathf.Sin(2.5f * t + 15f);
    }
    
    // Update is called once per frame
    public void Update()
    {
        float currentTime = Time.time;
        // add local noise, but how?
        float s = 1f + FourierNoise(2f * currentTime) * 0.2f + 0.2f;
        this.transform.localScale = new Vector3(s, s, 1);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.Rotate(Vector3.back, FourierNoise(5f * currentTime + 123f));
    }
}
