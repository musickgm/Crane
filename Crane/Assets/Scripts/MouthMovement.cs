using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles moving the mouth parent
/// </summary>
public class MouthMovement : MonoBehaviour
{
    public Vector3 variance;                                    //How much variance should the mouth have?
    public float speed;                                         //How fast should it move?
    public List<Collectable> teeth = new List<Collectable>();   //A list of all the teeth children
    public GameManager manager;                                 //Reference to the game manager

    private Vector3 position1;
    private Vector3 position2;

    /// <summary>
    /// Determine position 1 and 2 based on variance.
    /// </summary>
    private void Start()
    {
        position1 = transform.localPosition + variance;
        position2 = transform.localPosition - variance;
    }


    // Update is called once per frame
    void Update()
    {
        //Move after the initial wait if the game isn't over
        if(manager.initialWait || manager.gameOver)
        {
            return;
        }
        transform.localPosition = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    /// <summary>
    /// When a tooth is pulled (collectable grabbed), remove it from the list.
    /// </summary>
    /// <param name="tooth"></param>The tooth/collectable removed
    public void RemoveTooth(Collectable tooth)
    {
        teeth.Remove(tooth);
        //If no more teeth, turn this object off (hide tongue and stop moving)
        if(teeth.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
