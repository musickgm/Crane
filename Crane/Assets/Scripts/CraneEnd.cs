using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneEnd : MonoBehaviour
{
    public bool colliding = false;
    public CraneController craneController;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            colliding = true;
        }
        if(other.CompareTag("Objective"))
        {
            craneController.SetCurrentCollectable(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            colliding = false;
        }
    }

}
