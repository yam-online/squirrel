using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Count : MonoBehaviour
{
    public TMP_Text humanCount;

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
        }

    }
}
