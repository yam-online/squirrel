using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    private VisualElement healthBarProperty;
    public static Health instance {
        get;
        private set;
    }

    private void Awake() {
        instance = this;

        UIDocument uiDoc = GetComponent<UIDocument>();
        healthBarProperty = uiDoc.rootVisualElement.Q<VisualElement>("HealthBar");
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ResizeHealth(float resize) {
        healthBarProperty.style.width = Length.Percent(resize * 100.0f);
        // edge case: if negative --> game over!!, keep current health bar length
    }
}
