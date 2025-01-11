using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KutscheController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kutsche"))
        {
            // Spieler wird Child der Plattform

            // Debug.Log("Player is now a child of the platform.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Ahhhhhh");
        if (other.CompareTag("Kutsche"))
        {
            // Spieler wird von der Plattform entkoppelt

            // Debug.Log("Player is no longer a child of the platform.");
        }
    }
}