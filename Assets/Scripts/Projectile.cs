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
    public AudioClip collectedClip;

    public GameObject explodePrefab;

    public float lifetime = 10f;
    private float timer = 0f;

    private static int count = 0;

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
        // time based destroy
        timer += Time.deltaTime;
        if(timer >= lifetime) {
            Destroy(gameObject);
        }
        // distance based destroy
        else if(transform.position.magnitude > 100.0f) {
           Destroy(gameObject);
        }
    }

    public void Launch(Vector2 dir)
    {
        direction = dir.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        npc target = other.GetComponent<npc>();
        if(target != null) {
            if(explodePrefab != null) {
                GameObject explosion = Instantiate(explodePrefab, target.transform.position, Quaternion.identity);
                Destroy(explosion, 1.0f);
            }

            target.Damage(damage);
            if(target.health > 0) {
                target.PlaySound(collectedClip);
            }
            Destroy(gameObject);
            return;
        }

        SquirrelController squirrel = other.GetComponent<SquirrelController>();
        if(squirrel != null) {
            count++;
            squirrel.DecreaseFood();
            Destroy(gameObject);

            // if touched squirrel 5 times -> game over
            if(count == 5) {
                squirrel.enabled = false;
                squirrel.ShowExterminated();
                Time.timeScale = 0f;
            }
            return;
        }
    }
}

