using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Count : MonoBehaviour
{
    public TMP_Text humanCount;
    public TMP_Text livesCount;

    void Update()
    {
        if(npc.count == 1) {
            humanCount.text = $"{npc.count.ToString()} human left...";
        }
        else if(npc.count > 0) {
            humanCount.text = $"{npc.count.ToString()} humans left...";
        }
        else {
            humanCount.text = "Beware the exterminator...";
            int flippedCount = 5 - Projectile.count;
            if(flippedCount == 1) {
                livesCount.text = $"you have {flippedCount.ToString()} life left!";
            }
            else {
                livesCount.text = $"you have {flippedCount.ToString()} lives left!";

            }        
        }
    }
}
