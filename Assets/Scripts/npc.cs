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

    public static int count = 1;

    private bool isDead = false;

    public Canvas gameOverCanvas;

    void Start() {
        npcRB = GetComponent<Rigidbody2D>();
        currentHealth = 5;

        audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate() {
        if(isDead) {
            return;
        }

        float distance = Vector2.Distance(transform.position, squirrel.position);

        if(distance <= followRadius) {
            Vector2 pos = Vector2.MoveTowards(transform.position, squirrel.position, speed * Time.deltaTime);

            npcRB.MovePosition(pos);
        }
    }

    // if there is a collision --> game over!!!
    void OnTriggerEnter2D(Collider2D other) {
        if(isDead) {
            return;
        }
        SquirrelController squirrelGO = other.gameObject.GetComponent<SquirrelController>();
        if(squirrelGO != null) {
            // game over!
            squirrelGO.enabled = false;
            if(gameOverCanvas != null) {
                gameOverCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void Damage(int amount) {
        if(isDead) {
            return;
        }
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if(currentHealth == 0) {
            isDead = true;
            audioSource.PlayOneShot(deathClip, 3.0f);
            Destroy(gameObject, deathClip.length);
            npc.count--;
        }
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip, 3.0f);
    }
}
