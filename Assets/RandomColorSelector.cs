using UnityEngine;

public class RandomColorSelector : MonoBehaviour
{
    public MeshRenderer renderer;
    public Color[] colors;

    private void Start()
    {
        var color = colors[Random.Range(0, colors.Length)];
        renderer.material.color = color;
    }
}
