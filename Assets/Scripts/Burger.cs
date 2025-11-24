using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    private SquirrelController sc;
    public FoodType type;

    public AudioClip collectedClip;

    void Update() {
        if(sc != null && Input.GetKeyDown(KeyCode.Space)) {
            sc.PlaySound(collectedClip);
            sc.CollectFood(type);
            sc.ChangeHealth(3);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        SquirrelController controller = other.GetComponent<SquirrelController>();

        if(controller != null) {
            sc = controller;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        SquirrelController controller = other.GetComponent<SquirrelController>();

        if(controller == sc) {
            sc = null;
        }
    }
}
