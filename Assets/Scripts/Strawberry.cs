using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private SquirrelController sc;
    public FoodType type;

    void Update() {
        if(sc != null && Input.GetKeyDown(KeyCode.Space)) {
            sc.CollectFood(type);
            sc.ChangeHealth(2);
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
    // void OnTriggerEnter2D(Collider2D other) {
    //     SquirrelController controller = other.GetComponent<SquirrelController>();

    //     if(controller != null) {
    //         controller.ChangeHealth(2);
    //         Destroy(gameObject);
    //     }
    // }
}

