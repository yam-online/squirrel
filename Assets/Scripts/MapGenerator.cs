using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] Spawnables;
    public Vector2 BottomLeft, TopRight;
    public int count = 5;
    public float distance = 1f;

    private List<GameObject> activeObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < count; i++) {
            SpawnRandomObject();
        }
        
    }
    
    public void SpawnRandomObject() {
        int index = Random.Range(0, Spawnables.Length);
        SpawnObject(Spawnables[index]);
    }

    public void SpawnObject(GameObject prefab) {
        if(!Application.isPlaying) {
            return;
        }

        Vector2 pos;
        int tries = 0;

        do {
            pos = new Vector2(Random.Range(BottomLeft.x, TopRight.x), Random.Range(BottomLeft.y, TopRight.y));
            tries++;
        } while (Overlapping(pos) && tries < 5);

        GameObject GO = Instantiate(prefab, pos, Quaternion.identity);
        activeObjects.Add(GO);

        var notifyDestroyed = GO.AddComponent<Destroyed>();
        notifyDestroyed.generator = this;
        notifyDestroyed.prefab = prefab;
    }

    bool Overlapping(Vector2 pos) {
        foreach(GameObject GO in activeObjects) {
            if(GO == null) {
                continue;
            }
            if(Vector2.Distance(pos, GO.transform.position) < distance) {
                return true;
            }
        }
        return false;
    }

    public void Destroyed(GameObject GO, GameObject prefab) {
        activeObjects.Remove(GO);
        if(Application.isPlaying) {
            SpawnObject(prefab);
        }
    }
    
    private void OnDisable()
    {
        foreach(var obj in activeObjects) {
            if(obj != null) {
                Destroy(obj);
            }
        }
        activeObjects.Clear();
    }
}
