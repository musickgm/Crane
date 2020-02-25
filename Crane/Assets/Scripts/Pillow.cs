using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    public GameManager manager;

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
