using System.Collections.Generic;
using UnityEngine;

public class Busfahren : MonoBehaviour
{
    public GameObject actualObject;


    public float maxOffset = 0.5f;

    public List<Transform> busPassengers = new List<Transform>();

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = actualObject.transform.position;
    }

    void Update()
    {
        Vector3 movement = actualObject.transform.position - lastPosition;

        foreach (var player in busPassengers)
        {
            player.transform.position += movement;  // Passagiere bewegen sich mit der Plattform
        }

        lastPosition = actualObject.transform.position;  // Update die letzte Position der Plattform
    }

     

    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Bus beruhrt was");
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player steit ein");
            busPassengers.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         Debug.Log("aussteigen Bitte!");
        if (other.CompareTag("Player"))
        {
            busPassengers.Remove(other.transform);
            Debug.Log("Der Spieler ist ausgestiegen");
        }
    }

}
