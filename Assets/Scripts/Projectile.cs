using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;
    // public enum FoodType { Acorn, Strawberry, Burger }
    public FoodType type;
    private int damage;

    void Start() {
        switch(type) {
            case FoodType.Acorn:
                damage = 1;
                break;
            case FoodType.Strawberry:
                damage = 2;
                break;
            case FoodType.Burger:
                damage = 3;
                break;
        }
    }

    void Update() {
        if(transform.position.magnitude > 100.0f) {
           Destroy(gameObject);
        }
    }

    public void Launch(Vector2 dir)
    {
        direction = dir.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = direction * speed;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Add logic for hitting something (optional)
    //     Destroy(gameObject);
    // }

    void OnTriggerEnter2D(Collider2D other) {
        npc target = other.GetComponent<npc>();
        if(target != null) {
            target.Damage(damage);
            Destroy(gameObject);
        }
    }
}

