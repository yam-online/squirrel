using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public SquirrelController squirrel;
    public MapGenerator mapGenerator;
    public GameObject exterminator;
    public ExterminatorManager exterminatorManager;
    public Canvas cameraCanvas;
    public Canvas skullCanvas;
    public Canvas crownCanvas;

    void Start() {
        ResetGame();
    }
    public void ResetGame() {
        npc.count = 5;
        Projectile.count = 0;

        // squirrel
        if(squirrel != null) {
            squirrel.currentHealth = 0;
            squirrel.enabled = true;
        }

        // map generator
        if(mapGenerator != null) {
            foreach(var obj in mapGenerator.activeObjects) {
                if(obj != null) {
                    Destroy(obj);
                }
            }
            mapGenerator.activeObjects.Clear();
        }

        // exterminator
        if(exterminator != null) {
            exterminator.SetActive(false);
            exterminator.transform.position = Vector2.zero;
            var ext = exterminator.GetComponent<Exterminator>();
            if(ext != null) {
                ext.currentHealth = 15;
                if(ext.gameOverCanvas != null) {
                    ext.gameOverCanvas.gameObject.SetActive(false);
                }
            }
        }

        // exterminator manager
        if(exterminatorManager != null) {
            exterminatorManager.spawned = false;
        }
        
        // canvases
        if(cameraCanvas != null) {
            cameraCanvas.gameObject.SetActive(false);
        }
        if(skullCanvas != null) {
            skullCanvas.gameObject.SetActive(false);
        }
        if(crownCanvas != null) {
            crownCanvas.gameObject.SetActive(false);
        }

        // projectiles
        Projectile[] activeProj = FindObjectsOfType<Projectile>();
        foreach(var proj in activeProj) {
            Destroy(proj.gameObject);
        }
    }
}
