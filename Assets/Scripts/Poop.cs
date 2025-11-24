using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other) {
        SquirrelController controller = other.GetComponent<SquirrelController>();

        if(controller != null) {
            controller.PlaySound(collectedClip);
            controller.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}
