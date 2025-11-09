using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public Transform squirrel;
    public float speed = 1.0f;
    
    void Update() {
        transform.LookAt(squirrel);

        float dist = Vector3.Distance(transform.position, squirrel.position);
        if(dist > 1.0f) {
            // transform.position = Vector3.MoveTowards(transform.position, squirrel.position, speed);
            			transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

}
