using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public SomeoneDiedController someoneDiedController;

	public GameObject player;
    public Rigidbody2D playerBody;
    public float walkSpeed = 1.5f;
    public float jumpforce = 1.0f;
    public float jumpInterruptForce = 1.0f;
    public Collider2D groundIsTouched;
    public Transform PlayerFeet;
    public LayerMask canJumpOn;
    public int playerID; // 0 or 1

    public SpriteRenderer renderer;

    public TimeControlledObject travellingOnPlatform = null;
    
    // Controlls
    public KeyCode jump;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;

    // Level boundary
    public GameObject levelBoundary;

    public Sprite idleSprite;
    public Sprite jumpingSprite;

    public bool shouldFlip = false;
    public bool isWalking = false;

    private bool jumpKeyDown;
    private bool jumpKeyPressed;
    private bool jumpKeyReleased;

    private float coyoteTimeCounter = 0f; // Kurzzeitiges Springen nach Verlassen des Bodens
    private const float coyoteTime = 0.2f;

    private float jumpBufferCounter = 0f; // Sprung-Pufferung
    private const float jumpBufferTime = 0.2f;

    void Update()
    {
        // Überprüfung: Ist der Spieler auf dem Boden?
        groundIsTouched = Physics2D.OverlapCircle(PlayerFeet.position, 0.2f, canJumpOn);

        // Coyote-Zeit: Ermöglicht das Springen kurz nach Verlassen des Bodens
        if (groundIsTouched)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Sprung-Pufferung: Speichert Sprung-Eingaben für kurze Zeit
        if (Input.GetKeyDown(jump))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Springen
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpforce);
            jumpBufferCounter = 0;
        }

        // Sprung abbrechen (für kürzere Sprünge)
        if (Input.GetKeyUp(jump) && playerBody.velocity.y > 0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y * 0.5f);
        }

        // Ändern des Sprites basierend auf Bewegung
        renderer.sprite = groundIsTouched ? idleSprite : jumpingSprite;
        renderer.flipX = shouldFlip;
    }

    private void FixedUpdate()
    {
        // Horizontale Bewegung
        float horizontalInput = playerID == 0 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal P2");
        float targetSpeed = horizontalInput * walkSpeed;

        // Sanfte Anpassung der horizontalen Geschwindigkeit
        float speedDifference = targetSpeed - playerBody.velocity.x;
        float acceleration = Mathf.Abs(speedDifference) > 0.1f ? 10f : 15f; // Beschleunigung und Verzögerung
        float movement = speedDifference * acceleration * Time.fixedDeltaTime;

        playerBody.velocity = new Vector2(playerBody.velocity.x + movement, playerBody.velocity.y);

        // Flip Sprite basierend auf der Richtung
        if (horizontalInput != 0)
        {
            shouldFlip = horizontalInput < 0;
        }
    }

    private void Death()
    {
	    if (someoneDiedController.someoneDied)
	    {
		    return; // don't die again.
	    }
	    someoneDiedController.someoneDied = true;
		GetComponent<Rigidbody2D>().gravityScale = 0;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
		GlobalSceneManager.INSTANCE.ScheduleReloadScene(3.0f, "Spieler gestorben");
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == levelBoundary)
        {
            Debug.Log("Wir sind gestorben.");
            Death();
        }

		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Wir sind durch einen Gegner gestorben.");
            Death();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Wir sind durch einen Gegner gestorben.");
            Death();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("Wir sind durch einen Gegner gestorben.");
			Death();
		}
	}
}
