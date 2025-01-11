using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSkyController : MonoBehaviour
{
    public TimeController timeController;

    public float skyMinAlpha;
    public float skyMaxAlpha;

    public SpriteRenderer skyRenderer;

    public void Update()
    {
        Color color = skyRenderer.color;
        float alpha = 1f;
        if (timeController.isTimeChanging)
        {
            var t = Time.time;
            t = Mathf.Sin(15f * t) + 0.3f * Mathf.Sin(20f * t) + 0.1f * Mathf.Sin(30f * t);
            alpha = Mathf.Lerp(skyMinAlpha, skyMaxAlpha, (t + 1f) / 2f);
        }
        color.a = alpha;
        skyRenderer.color = color;
    }
}
