using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMvmt : MonoBehaviour
{
    public Transform squirrel;
    public Vector3 offset; // set to 0,0,-10

    void Update()
    {
        transform.position = squirrel.position + offset;
    }
}
