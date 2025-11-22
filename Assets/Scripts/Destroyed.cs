using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed : MonoBehaviour
{
    [HideInInspector] public MapGenerator generator;
    [HideInInspector] public GameObject prefab;

    private void OnDestroy() {
        if(generator != null && Application.isPlaying) {
            generator.Destroyed(gameObject, prefab);
        }
    }
}
