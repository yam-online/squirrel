using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void Launch(Vector2 dir)
    {
        direction = dir.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Add logic for hitting something (optional)
        Destroy(gameObject);
    }
}

