using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accounts for teeth falling off of the map - destroys them.
/// </summary>
public class ToothDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Objective"))
        {
            Destroy(other.gameObject);
        }
    }
}
