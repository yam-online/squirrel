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
    public int currentHealth;
    public int health {
        get {
            return currentHealth;
        }
    }

    public GameObject acornProjectilePrefab;
    public GameObject strawberryProjectilePrefab;
    public GameObject burgerProjectilePrefab;
    private List<GameObject> collected = new List<GameObject>();

    AudioSource audioSource;

    public Canvas exterminatedCanvas;

    public float coolDown = 0.6f;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = 0;
        MoveAction.Enable();
        rigidbody = GetComponent<Rigidbody2D>();

        Health.instance.ResizeHealth((float)currentHealth / (float)maxHealth);

        audioSource = GetComponent<AudioSource>();
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

        if(Mouse.current.leftButton.wasPressedThisFrame) {
            ThrowTowardsMouse();
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

    public void CollectFood(FoodType type) {
        switch(type) {
            case FoodType.Acorn:
                collected.Add(acornProjectilePrefab);
                break;
            case FoodType.Strawberry:
                collected.Add(strawberryProjectilePrefab);
                break;
            case FoodType.Burger:
                collected.Add(burgerProjectilePrefab);
                break;
        }
    }

    public void DecreaseFood() {
        if(collected.Count == 0) {
            // ChangeHealth(-1);
            return;
        }

        GameObject removedType = collected[0];
        if(removedType.name.Contains("acorn")) {
            ChangeHealth(-1);
        }
        else if(removedType.name.Contains("strawberry")) {
            ChangeHealth(-2);
        }
        else if(removedType.name.Contains("burger")) {
            ChangeHealth(-3);
        }
        collected.RemoveAt(0);
    }

    void ThrowTowardsMouse() {
        if(Time.time < timer) {
            return;
        }
        if(collected.Count == 0 || currentHealth < 0) {
            return;
        }
        timer = Time.time + coolDown;

        GameObject prefab = collected[0];
        collected.RemoveAt(0);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        Vector2 spawnOffset = direction * 0.5f;
        Vector2 spawnPos = (Vector2)transform.position + spawnOffset;
        GameObject projectile = Instantiate(prefab, spawnPos, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        // determine foodtype based on prefab
        if(prefab.name.Contains("acorn")) {
            projScript.type = FoodType.Acorn;
            ChangeHealth(-1);
        }
        else if(prefab.name.Contains("strawberry")) {
            projScript.type = FoodType.Strawberry;
            ChangeHealth(-2);
        }
        else if(prefab.name.Contains("burger")) {
            projScript.type = FoodType.Burger;
            ChangeHealth(-3);
        }

        projScript.Launch(direction);
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip, 6.0f);
    }

    public void ShowExterminated() {
        if(exterminatedCanvas != null) {
            exterminatedCanvas.gameObject.SetActive(true);
        }
        else {Debug.Log("cant find it");}
    }
}
