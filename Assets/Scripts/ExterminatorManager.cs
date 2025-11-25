using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExterminatorManager : MonoBehaviour
{
    public GameObject exterminator;
    public Transform squirrel;
    public float speed = 0.9f;
    private bool spawned = false;

    void Update()
    {
        if(npc.count == 6 && !spawned) {
            spawned = true;
            StartCoroutine(SpawnExterminator());
        }
        if(exterminator.activeSelf) {
            if(!squirrel) {
                return;
            }

            Vector2 targetPos = squirrel.position;
            Vector2 newPos = Vector2.MoveTowards(exterminator.transform.position, targetPos, speed * Time.deltaTime);
            exterminator.transform.position = newPos;

            SpriteRenderer exterminatorSR = exterminator.GetComponent<SpriteRenderer>();
            if(exterminatorSR) {
                if(squirrel.position.x < exterminator.transform.position.x) {
                    exterminatorSR.flipX = true;
                }
                else {
                    exterminatorSR.flipX = false;
                }
            }
        }
    }

    IEnumerator SpawnExterminator() {
        yield return new WaitForSeconds(2f);

        if(exterminator != null) {
            exterminator.transform.position = Vector2.zero;
            exterminator.SetActive(true);
        }
    }
}
