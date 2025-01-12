using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player; // Reference to the player GameObject
    [SerializeField] private GameObject _levelBoundary; // Reference to the level boundary (with BoxCollider)

    private BoxCollider _boundaryCollider; // BoxCollider component of the level boundary
    private float _camHalfHeight; // Half the height of the camera (orthographic mode)
    private float _camHalfWidth; // Half the width of the camera (orthographic mode)

    private void Start()
    {
        // Ensure the level boundary has a BoxCollider
        if (_levelBoundary != null)
        {
            _boundaryCollider = _levelBoundary.GetComponent<BoxCollider>();
            if (_boundaryCollider == null)
            {
                Debug.LogError("No BoxCollider found on the level boundary object!");
                return;
            }
        }
        else
        {
            Debug.LogError("Level Boundary GameObject not assigned!");
            return;
        }

        // Calculate the camera's dimensions
        Camera cam = Camera.main;
        if (cam.orthographic)
        {
            _camHalfHeight = cam.orthographicSize;
            _camHalfWidth = _camHalfHeight * cam.aspect;
        }
        else
        {
            Debug.LogError("This script is designed for orthographic cameras only!");
        }
    }

	private void Update()
    {
        // Ensure the player is assigned
        if (_player != null && _boundaryCollider != null)
        {
            // Get the player's position
            Vector3 playerPosition = _player.transform.position;

            // Set the desired camera position with an offset
            Vector3 targetPosition = new Vector3(playerPosition.x, playerPosition.y, -15);

            // Clamp the target position within the level boundaries
            float clampedX = Mathf.Clamp(
                targetPosition.x,
                _boundaryCollider.bounds.min.x + _camHalfWidth,
                _boundaryCollider.bounds.max.x - _camHalfWidth
            );

            float clampedY = Mathf.Clamp(
                targetPosition.y,
                _boundaryCollider.bounds.min.y + _camHalfHeight,
                _boundaryCollider.bounds.max.y - _camHalfHeight
            );

            // Apply the clamped position to the camera
            transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }
}