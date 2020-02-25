using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player movement and rotation using player input
/// </summary>
public class CraneController : MonoBehaviour
{
    public float j1Speed = 4;             //How fast does the player move
    public float j2Speed = 1;       //Sensitivity of rotation
    public Transform j1X;
    public Transform j1Y;
    public Transform j2;
    public float l1Length = 2;
    public float l2Length = 2;
    public float j3Length = .5f;
    public CraneEnd craneEnd;

    private Transform currentCollectable = null;
    private bool gameOver = false;



    /// <summary>
    /// Move and rotate the player every frame
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentCollectable != null)
        {
            currentCollectable = null;
        }

        //Player input + other factors
        float j1RotY = Input.GetAxis("Mouse X") * Time.deltaTime * j1Speed;
        float j1RotX = Input.GetAxis("Mouse Y") * Time.deltaTime * j1Speed;
        float j2RotX = Input.GetAxis("Vertical")  * Time.deltaTime * j2Speed;


        //Rotate the player
        j1Y.Rotate(0, j1RotY, 0);

        if(craneEnd.colliding)
        {
            if(j2RotX > 0 || j1RotX >0)
            {
                return;
            }
        }
        j1X.Rotate(j1RotX, 0, 0);
        j2.Rotate(j2RotX, 0, 0);


        //Prevent bad movements/rotations with clamping
        Clamp();
    }

    private void LateUpdate()
    {
        if (currentCollectable != null)
        {
            MoveCollectable();
        }
    }

    /// <summary>
    /// Clamp the player rotations and position to prevent wall climbing and unintentional x/z rotations
    /// </summary>
    private void Clamp()
    {
        float xRot = j1X.localEulerAngles.x;
        if (j1X.localEulerAngles.x >= 90 && j1X.localEulerAngles.x < 180)
        {
            xRot = 90;
        }
        else if(j1X.localEulerAngles.x > 180)
        {
            xRot = 0;
        }
        j1X.localEulerAngles = new Vector3(xRot, 0, 0);


        float xRotJ2 = j2.localEulerAngles.x;
        if (xRotJ2 > 300)
        {
            xRotJ2 = 0;
            j2.localEulerAngles = new Vector3(xRotJ2, 0, 0);

        }

    }

    /// <summary>
    /// When the game is over, change the bool
    /// </summary>
    public void SetGameOver()
    {
        gameOver = true;
    }

    public void SetCurrentCollectable(Transform _collectable)
    {
        if(currentCollectable != null || gameOver)
        {
            return;
        }
        currentCollectable = _collectable;
        _collectable.GetComponent<Collectable>().GrabObject();
    }

    private void MoveCollectable()
    {
        Vector3 l1Translation = new Vector3(0, l1Length, 0);
        Vector3 l2Translation = new Vector3(0, l2Length + j3Length, 0);

        Matrix4x4 l1Matrix = Matrix4x4.Translate(l1Translation);
        Matrix4x4 l2Matrix = Matrix4x4.Translate(l2Translation);
        Matrix4x4 j1YMatrix = Matrix4x4.Rotate(j1Y.localRotation);
        Matrix4x4 j1XMatrix = Matrix4x4.Rotate(j1X.localRotation);
        Matrix4x4 j2Matrix = Matrix4x4.Rotate(j2.localRotation);

        Matrix4x4 finalMatrix =  j1YMatrix * j1XMatrix * l1Matrix * j2Matrix * l2Matrix;
        currentCollectable.position = new Vector3(finalMatrix[0, 3], finalMatrix[1, 3], finalMatrix[2, 3]);
        currentCollectable.rotation = finalMatrix.rotation;

    }
}

