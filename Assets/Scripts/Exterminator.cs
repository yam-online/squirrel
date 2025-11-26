using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exterminator : MonoBehaviour
{
    public int currentHealth = 5;
    public int health {
        get {
            return currentHealth;
        }
    }

    AudioSource audioSource;
    public AudioClip deathClip;

    public Canvas gameOverCanvas;
    public Canvas exterminatedCanvas;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(int amount) {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if(currentHealth == 0) {
            audioSource.PlayOneShot(deathClip);
            Destroy(gameObject, deathClip.length);
            if(gameOverCanvas != null) {
                gameOverCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    
    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    void OnTriggerEnter2D(Collider2D other) {
        SquirrelController squirrelGO = other.gameObject.GetComponent<SquirrelController>();
        if(squirrelGO != null) {
            // game over!
            squirrelGO.enabled = false;
            if(exterminatedCanvas != null) {
                exterminatedCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
