using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to account for all types of collectables
/// </summary>
public class Collectable : MonoBehaviour
{
    public float collectableValue;
    public AudioClip _clip;                             //Audio clip associated with collecting this item
    public MouthMovement mouthParent;
    public GameManager manager;


    public void GrabObject()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        mouthParent.RemoveTooth(this);
        manager.PlayAudio(_clip);
    }
}
