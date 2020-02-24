using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthMovement : MonoBehaviour
{
    public Vector3 variance;
    public float speed;

    private Vector3 position1;
    private Vector3 position2;

    private void Start()
    {
        position1 = transform.localPosition + variance;
        position2 = transform.localPosition - variance;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
