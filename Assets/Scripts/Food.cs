using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FoodType {
    Acorn,
    Strawberry,
    Burger
}

public class Food : MonoBehaviour
{
    private SquirrelController sc;
    public FoodType type;

    void Update() {
        if(sc != null && Input.GetKeyDown(KeyCode.Space)) {
            sc.CollectFood(type);
            sc.ChangeHealth(1);
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
