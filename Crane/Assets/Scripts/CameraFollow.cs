using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows and looks at the player using spring type motion for smoothing. 
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform player;                    //The object/player to follow
    public Vector3 offset;                      //Position offset of the camera
    public float springConstantPos = 0.05f;     //Spring constant for position
    public float springConstantRot = 80f;       //Spring constant for rotation


    /// <summary>
    /// Lerps both position and rotation smoothly and follows/looks at the player
    /// </summary>
    void LateUpdate()
    {
        Quaternion goalRotation = LerpRotation();

        Vector3 goalPosition = player.position + (goalRotation * offset);


        Vector3 lerpPosition = Vector3.Lerp(transform.position, goalPosition, springConstantPos);


        transform.position = lerpPosition;
        transform.LookAt(player);
    }


    /// <summary>
    /// Rotates/lerps the camera around the player smoothly
    /// </summary>
    /// <returns></returns>
    private Quaternion LerpRotation()
    {
        float currentY = transform.eulerAngles.y;
        float goalY = player.transform.eulerAngles.y;
        float lerpAngle = Mathf.LerpAngle(currentY, goalY, Time.deltaTime * springConstantRot);

        return  Quaternion.Euler(0, lerpAngle, 0); ;
    }

}
