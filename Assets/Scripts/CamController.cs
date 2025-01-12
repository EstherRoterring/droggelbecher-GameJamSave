using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player; // Reference to the player GameObject

    private void Update()
    {
        // Ensure the player is assigned
        if (_player != null)
        {
            // Get the player's position
            Vector3 playerPosition = _player.transform.position;

            // Set the camera position to the player's position with an offset
            transform.position = new Vector3(playerPosition.x, playerPosition.y, -15);
        }
    }
}