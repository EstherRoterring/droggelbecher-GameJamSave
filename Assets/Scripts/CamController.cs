using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D boundary;
    public Camera myCamera;

    public float cameraSpeed;

	private void Update()
    {
        var cameraZ = myCamera.transform.position.z;
        var oldPosition = myCamera.transform.position;
        var diff = player.transform.position - oldPosition;
        if (diff.magnitude <= cameraSpeed * Time.deltaTime)
        {
            myCamera.transform.position = player.transform.position;
        } else
        {
            myCamera.transform.position = oldPosition + cameraSpeed * Time.deltaTime * diff;
        }

        // clamping

        float halfHeight = myCamera.orthographicSize;
        float halfWidth = myCamera.aspect * halfHeight;
        Debug.Log("Camera" + halfWidth + "x" + halfHeight);
        
        myCamera.transform.position = Vector3.Max(boundary.bounds.min + new Vector3(halfWidth, halfHeight, 0), myCamera.transform.position);
        myCamera.transform.position = Vector3.Min(boundary.bounds.max - new Vector3(halfWidth, halfHeight,0), myCamera.transform.position);

        // reset z
        myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, cameraZ);
        
    }
}