using System;
using UnityEngine;

public class PlatformColorAssigner : MonoBehaviour
{
    public Color[] staticPlatformColors;
    public Color[] movingPlatformColors;

    private void Awake()
    {
        foreach (var comp in GetComponentsInChildren<RandomColorSelector>())
        {
            ColliderOnPlatform movingPlatform;
            if (comp.transform.parent.TryGetComponent<ColliderOnPlatform>(out movingPlatform))
            {
                comp.colors = movingPlatformColors;
            } else {
                comp.colors = staticPlatformColors;
            }
        }
    }

    private void Update()
    {
        Debug.Log(GetComponentsInChildren<RandomColorSelector>().Length);
    }
}
