using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles items being dropped and collected (scored)
/// </summary>
public class Pillow : MonoBehaviour
{
    public GameManager manager;                 //Reference to the game manager

    /// <summary>
    /// When an item is dropped, destroy it and tell the manager to handle its collection
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Objective"))
        {
            float value = other.GetComponent<Collectable>().collectableValue;
            Destroy(other.gameObject);
            manager.CollectItem(value);
        }
    }
}
