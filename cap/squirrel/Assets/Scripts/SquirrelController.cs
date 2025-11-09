using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquirrelController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbody;
    Vector2 move;
    public float speed = 3.0f;

    public int maxHealth;
    int currentHealth;
    public int health {
        get {
            return currentHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = 0;
        MoveAction.Enable();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        // moving right
        if(Input.GetKey(KeyCode.D)) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        // moving left
        else if(Input.GetKey(KeyCode.A)) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody.position + move * speed * Time.deltaTime;
        rigidbody.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        currentHealth = currentHealth + amount;
        
        Health.instance.ResizeHealth((float)currentHealth / (float)maxHealth);
        Debug.Log(maxHealth);
        Debug.Log((float)currentHealth / (float)maxHealth);
    }
}
