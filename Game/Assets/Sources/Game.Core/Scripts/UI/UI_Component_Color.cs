using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Component_Color : Module
{
    public Image img_selected = default;
    public Image img = default;
    public string hex = "#FFF";

    private void Awake()
    {
        img_selected.enabled = false;    
    }

    protected override void OnSubscription(bool condition)
    {
        Middleware<string>.Subscribe_Publish(condition, "Color_Selected", Active);
    }

    public void Active(string _hex) => img_selected.enabled = this.hex.Equals(_hex);

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

    public void OnSelected()
    {
        Middleware<string>.Invoke_Publish("Color_Selected", hex);
    }
}
