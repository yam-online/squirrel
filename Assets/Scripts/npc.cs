using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public Transform squirrel;
    public float speed = 0.5f;
    public float followRadius = 20f;
    Rigidbody2D npcRB;

    int currentHealth;
    public int health {
        get {
            return currentHealth;
        }
    }

    AudioSource audioSource;
    public AudioClip deathClip;

    void Start() {
        npcRB = GetComponent<Rigidbody2D>();
        currentHealth = 5;

        audioSource = GetComponent<AudioSource>();
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

    public void Damage(int amount) {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if(currentHealth == 0) {
            audioSource.PlayOneShot(deathClip);
            Debug.Log(deathClip.length);
            Destroy(gameObject, deathClip.length);
        }
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
