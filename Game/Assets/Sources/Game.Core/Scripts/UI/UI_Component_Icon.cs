using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Component_Icon : Module
{
    public Image img_selected = default;
    public Image img = default;
    public Sprite sprite = default;
    public int index = default;

    private void Awake()
    {
        img_selected.enabled = false;    
    }

    protected override void OnSubscription(bool condition)
    {
        Middleware<string>.Subscribe_Publish(condition, "Color_Selected", Color_Selected);

        Middleware<int>.Subscribe_Publish(condition, "Icon_Selected", Active);
    }

    public void Active(int _index) => img_selected.enabled = this.index.Equals(_index);

    public void Color_Selected(string _hex)
    {
        if (ColorUtility.TryParseHtmlString(_hex, out Color _color))
        {
            this.img.color = _color;
        }
    }


    public void Set(in int _index)
    {
        index = _index;
        img.sprite = GameManager._.list_character_sprites[_index];
        img_selected.sprite = GameManager._.list_character_sprites[_index];
    }

    public void OnSelected()
    {
        Middleware<int>.Invoke_Publish("Icon_Selected", index);
    }
}
