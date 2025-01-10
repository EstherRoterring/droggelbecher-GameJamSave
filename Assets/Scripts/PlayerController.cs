using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public float walkSpeed = 1.5f;
    public float jumpforce;
    public bool groundIsTouched;
    public Transform PlayerFeet;
    public LayerMask canJumpOn;
    
    //Controlls
    public KeyCode jump;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Abfrage ob player auf boden
        groundIsTouched = Physics2D.OverlapCircle(PlayerFeet.position, 0.5f , canJumpOn);
        
        //WASD Steuerung
        if (Input.GetKey(jump) && groundIsTouched){
            transform.Translate(0f, 1f * (jumpforce) * Time.deltaTime,0f);
        }
        if (Input.GetKey(left)){
            transform.Translate(1f * (-walkSpeed) * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(down)){
            transform.Translate(0f, 1f * (-walkSpeed) * Time.deltaTime,0f);
        }
        if (Input.GetKey(right)){
            transform.Translate(1f * (walkSpeed) * Time.deltaTime, 0f, 0f);
        }
    }


}

