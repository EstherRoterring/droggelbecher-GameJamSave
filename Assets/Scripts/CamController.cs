using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D boundary;
    public Camera myCamera;

    public float cameraSpeed;

	private void Update()
    {
        var oldPosition = myCamera.transform.position;
        var diff = player.transform.position - oldPosition;
        if (diff.magnitude <= cameraSpeed * Time.deltaTime)
        {
            myCamera.transform.position = player.transform.position;
        } else
        {
            myCamera.transform.position = oldPosition + cameraSpeed * Time.deltaTime * diff;
        }
    }
}