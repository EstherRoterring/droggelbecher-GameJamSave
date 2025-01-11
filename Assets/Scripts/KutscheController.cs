using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KutscheController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ahhhhhh");
        if (other.CompareTag("Kutsche"))
        {
            // Spieler wird Child der Plattform
            transform.SetParent(other.transform);
            Debug.Log("Player is now a child of the platform.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Kutsche"))
        {
            // Spieler wird von der Plattform entkoppelt
            transform.SetParent(null);
            Debug.Log("Player is no longer a child of the platform.");
        }
    }
}
