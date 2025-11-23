using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public Transform squirrel;
    public float speed = 0.5f;
    public float followRadius = 20f;
    Rigidbody2D npcRB;

    void Start() {
        npcRB = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate() {
        float distance = Vector2.Distance(transform.position, squirrel.position);

        if(distance <= followRadius) {
            Vector2 pos = Vector2.MoveTowards(transform.position, squirrel.position, speed * Time.deltaTime);

            npcRB.MovePosition(pos);
        }
    }

    // if there is a collision --> game over!!!
    void OnTriggerEnter2D(Collider2D other) {
        SquirrelController squirrelGO = other.gameObject.GetComponent<SquirrelController>();
        if(squirrelGO != null) {
            squirrelGO.ChangeHealth(-10);
            // game over not implemented yet
        }
    }
}
