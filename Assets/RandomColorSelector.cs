using UnityEngine;

public class RandomColorSelector : MonoBehaviour
{
    public new MeshRenderer renderer = null;
    public Color[] colors;

    private bool enabled = false;
    
    public void Update()
    {
        if (enabled)
        {
            return;
        }
        var color = colors[Random.Range(0, colors.Length)];
        renderer.material.color = color;
        enabled = true;
    }
}
