using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneEnd : MonoBehaviour
{
    public bool colliding = false;
    public CraneController craneController; 
    public Transform j1;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            colliding = true;
        }
        if(other.CompareTag("Objective"))
        {
            craneController.SetCurrentCollectable(other.transform);


            //Vector3 translation = new Vector3(0, 2, 0);
            //Quaternion rotation = j1.rotation;
            //Matrix4x4 m = Matrix4x4.Translate(translation);
            //Matrix4x4 n = Matrix4x4.Rotate(rotation);
            //Matrix4x4 o = n * m;


            //currentObjective.position = new Vector3(o[0, 3], o[1, 3], o[2, 3]);
        //currentObjective.position = Matrix4x4
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
