using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Component_Color : MonoBehaviour
{
    [SerializeField] private Image img = default;
    [SerializeField] private string hex = "#FFF";

    public void Set(in string _hex)
    {
        if (ColorUtility.TryParseHtmlString(_hex, out Color _color))
        {
            this.img.color = _color;
            this.hex = _hex;
        }
        else
        {
            Debug.LogWarning($"Fallo en crear color {this.hex} en {name}");
        }
    }
}
