using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to handle the end of the crane - picking up objects and avoiding going
/// underground
/// </summary>
public class CraneEnd : MonoBehaviour
{
    public CraneController craneController;     //CraneController script reference
    [HideInInspector]
    public bool colliding = false;              //Are we colliding with the ground?


    private void OnTriggerEnter(Collider other)
    {
        //See if we're colliding with ourself or the ground (prevent bad rotations)
        if(other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            colliding = true;
        }
        //See if we're colliding with a collectable
        if(other.CompareTag("Objective"))
        {
            craneController.SetCurrentCollectable(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If we're done colliding, set bool to false
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            colliding = false;
        }
    }

}
