using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshOutline : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Color32 color;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.outlineWidth = 0.6f;
        textMeshPro.outlineColor = color;
    }
}
