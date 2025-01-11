using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerBody;
    public float walkSpeed = 1.5f;
    public float jumpforce;
    public Collider2D groundIsTouched;
    public Transform PlayerFeet;
    public LayerMask canJumpOn;
    public int playerID; // 0 or 1

    public SpriteRenderer renderer;

    public TimeControlledObject travellingOnPlatform = null;
    
    //Controlls
    public KeyCode jump;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;

    // level boundary
    public GameObject levelBoundary;

    public Sprite idleSprite;
    public Sprite jumpingSprite;

    public bool shouldFlip = false;
    public bool isWalking = false;

    private bool jumpKeyDown;
    private bool jumpkeyPressed;
    private bool jumpKeyReleased;

    // Update is called once per frame
    void Update()
    {
		//Abfrage ob player auf boden
		if (travellingOnPlatform)
        {
            Debug.Log("nicht mehr drauf?");
            travellingOnPlatform.busPassengers.Remove(this.transform);
        }
        groundIsTouched = Physics2D.OverlapCircle(PlayerFeet.position, 0.2f, canJumpOn);
        travellingOnPlatform = null;
        if (groundIsTouched)
        {
            ColliderOnPlatform colliderOnPlatform = null;
            if (groundIsTouched.gameObject.TryGetComponent<ColliderOnPlatform>(out colliderOnPlatform))
            {
                travellingOnPlatform = colliderOnPlatform.platform;
                // travellingOnPlatform.busPassengers.Add(this.transform);  // todo, add me again
            }
        }

        // Jump key states
        float jumpAxis = playerID == 0 ? Input.GetAxis("Jump") : Input.GetAxis("Jump P2");

        if (Input.GetKey(jump) && groundIsTouched)
        {
            playerBody.AddForce(Vector2.up * jumpforce);
        }
        
        // change sprites
        if (groundIsTouched)
        {
            renderer.sprite = idleSprite;
        }
        else
        {
            renderer.sprite = jumpingSprite;
        }
        renderer.flipX = shouldFlip;
    }

	private void FixedUpdate()
	{
        // Horizontal Movement
        float horizontal_movement = playerID == 0 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal P2");

        // maybe very shit
        playerBody.velocity = new Vector2(horizontal_movement * walkSpeed, playerBody.velocity.y);

        // flip sprite
        shouldFlip = playerBody.velocity.x < 0;
	}

	private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == levelBoundary)
        {
            Debug.Log("We died.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
