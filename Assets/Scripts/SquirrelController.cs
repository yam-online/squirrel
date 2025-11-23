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

    public GameObject acornProjectilePrefab;
    // public GameObject strawberryProjectilePrefab;
    // public GameObject burgerProjectilePrefab;
    private List<GameObject> collected = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = 0;
        MoveAction.Enable();
        rigidbody = GetComponent<Rigidbody2D>();

        Health.instance.ResizeHealth((float)currentHealth / (float)maxHealth);
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

        if(Input.GetKeyDown(KeyCode.F)) {
            Throw();
        }
    }

    void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody.position + move * speed * Time.deltaTime;
        rigidbody.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        currentHealth = currentHealth + amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        Health.instance.ResizeHealth((float)currentHealth / (float)maxHealth);
        Debug.Log((float)currentHealth / (float)maxHealth);
    }

    public void CollectFood(GameObject projectilePrefab) {

    if (projectilePrefab == null) {
        Debug.LogError("🚨 ERROR: projectilePrefab is NULL when collecting!");
    }

    collected.Add(projectilePrefab);

    Debug.Log("🧺 CURRENT COLLECTED LIST:");
    for (int i = 0; i < collected.Count; i++) {
        if (collected[i] == null) {
            Debug.LogError($"❌ Slot {i} = NULL (broken reference)");
        } else {
            Debug.Log($"✔ Slot {i}: {collected[i].name}");
        }
    }
    }

    public void CollectFood(FoodType type) {
        switch(type) {
            case FoodType.Acorn:
                collected.Add(acornProjectilePrefab);
                break;
            // case FoodType.Strawberry:
            //     collected.Add(strawberryProjectilePrefab);
            //     break;
            // case FoodType.Burger:
            //     collected.Add(burgerProjectilePrefab);
            //     break;
        }

        for (int i = 0; i < collected.Count; i++) {
        if (collected[i] == null) {
            Debug.LogError($"❌ Slot {i} = NULL (broken reference)");
        } else {
            Debug.Log($"✔ Slot {i}: {collected[i].name}");
        }
        }
    }

    public void Throw() {
        // nothing is collected yet
        if(collected.Count == 0) {
            return;
        }

        // retrieve first item in queue
        GameObject projectilePrefab = collected[0];
        // remove it
        collected.RemoveAt(0);

        // instantiate the prefab as a gameobject to be able to see it on screen
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        Vector2 direction = Vector2.right;
        if(GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            direction = GetComponent<Rigidbody2D>().velocity.normalized;

        projScript.Launch(direction);
    }
}
