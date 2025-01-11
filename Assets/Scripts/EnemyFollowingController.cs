using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFollowingController : MonoBehaviour
{

    public float speed = 2;
    public Transform target;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //aktuelle Rotation bekommen -> standard
        rotation = transform.eulerAngles;


    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //umdrehen
        float a = transform.position.x;
        float b = target.position.x;
        float richtung = a - b;
        Debug.Log(richtung);

        if (richtung < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
        }
        if (richtung > 0)
        {
            transform.eulerAngles = rotation;
        }





    }
}
