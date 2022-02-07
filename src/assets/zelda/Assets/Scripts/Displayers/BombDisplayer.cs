using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombDisplayer : MonoBehaviour
{
    public Inventory inventory;
    TMP_Text text_component;
    // Start is called before the first frame update
    void Start()
    {
        text_component = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = "Bombs: " + inventory.GetBombs().ToString();
        }
    }
}