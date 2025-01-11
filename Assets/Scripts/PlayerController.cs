using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerBody;
    public float walkSpeed = 1.5f;
    public float jumpforce;
    public bool groundIsTouched;
    public bool plattformIsTouched;
    public Transform PlayerFeet;
    public LayerMask canJumpOn;
    
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
        //Abfrage ob player auf boden
        groundIsTouched = Physics2D.OverlapCircle(PlayerFeet.position, 0.1f, canJumpOn);

        //WASD Steuerung
        if (Input.GetKey(jump) && groundIsTouched)
        {
            playerBody.AddForce(Vector2.up * jumpforce * Time.deltaTime);
        }

        if (Input.GetKey(left))
        {
            playerBody.AddForce(Vector2.left * walkSpeed * Time.deltaTime);
        }

        if (Input.GetKey(down))
        {
            playerBody.AddForce(Vector2.down * walkSpeed * Time.deltaTime);
        }

        if (Input.GetKey(right))
        {
            playerBody.AddForce(Vector2.right * walkSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("We left a trigger");
        if (other.gameObject == levelBoundary)
        {
            Debug.Log("player dies!!!");
        }
    }


}

