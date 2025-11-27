using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exterminator : MonoBehaviour
{
    public int currentHealth = 15;
    public int health {
        get {
            return currentHealth;
        }
    }

    AudioSource audioSource;
    public AudioClip deathClip;

    public AudioClip loseClip;

    public Canvas gameOverCanvas;
    public Canvas exterminatedCanvas;

    public Canvas speechBubbleCanvas;
    public TMPro.TextMeshProUGUI speechText;
    public float duration = 2f;
    string[] sayings = new string[] {
        "Curse you dumb squirrel!",
        "I'm gonna get you!",
        "Don't make me angry!",
        "Hey, you ruined my favorite shirt!",
        "Yowch! That hurt!"
    };

    void Start() {
        audioSource = GetComponent<AudioSource>();
        speechBubbleCanvas.gameObject.SetActive(false);
        currentHealth = 15;
    }

    public void Damage(int amount) {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        ShowSpeechBubble();

        if(currentHealth == 0) {
            audioSource.PlayOneShot(deathClip, 0.2f);
            Destroy(gameObject, deathClip.length);
            if(gameOverCanvas != null) {
                gameOverCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    
    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip, 3.0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        SquirrelController squirrelGO = other.gameObject.GetComponent<SquirrelController>();
        if(squirrelGO != null) {
            // game over!
            squirrelGO.enabled = false;
            audioSource.PlayOneShot(loseClip, 0.7f);
            if(exterminatedCanvas != null) {
                exterminatedCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void ShowSpeechBubble() {
        if(speechBubbleCanvas == null) {
            return;
        }

        int i = Random.Range(0, sayings.Length);
        speechText.text = sayings[i];

        speechBubbleCanvas.gameObject.SetActive(true);
        
        StopAllCoroutines();
        StartCoroutine(HideBubbleAfterDelay());
    }

    IEnumerator HideBubbleAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        speechBubbleCanvas.gameObject.SetActive(false);
    }
}
