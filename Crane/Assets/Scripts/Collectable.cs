using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to account for collectables
/// </summary>
public class Collectable : MonoBehaviour
{
    public float collectableValue;                      //How much is this collectable worth?
    public AudioClip _clip;                             //Audio clip associated with collecting this item
    public MouthMovement mouthParent;
    public GameManager manager;


    /// <summary>
    /// Account for everything that needs to happen when an object is picked up. 
    /// </summary>
    public void GrabObject()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        mouthParent.RemoveTooth(this);
        manager.PlayAudio(_clip);
        GetComponent<BoxCollider>().enabled = false;
    }


}
