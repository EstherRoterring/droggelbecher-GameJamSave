using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public TimeControlledObject travellingOnPlatform = null;
    
    //Controlls
    public KeyCode jump;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;

    // level boundary
    public GameObject levelBoundary;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print


        //Abfrage ob player auf boden
        if (travellingOnPlatform)
        {
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
                travellingOnPlatform.busPassengers.Add(this.transform);
            }
        }

        //WASD Steuerung
        if (Input.GetKey(jump) && groundIsTouched)
        {
            playerBody.AddForce(Vector2.up * jumpforce);
        }

        if (Input.GetKey(left))
        {
            playerBody.AddForce(Vector2.left * walkSpeed);
        }

        if (Input.GetKey(down))
        {
            playerBody.AddForce(Vector2.down * walkSpeed);
        }

        if (Input.GetKey(right))
        {
            playerBody.AddForce(Vector2.right * walkSpeed);
        }
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
