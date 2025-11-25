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
        humanCount.text = $"{npc.count.ToString()} humans left...";
    }
}
