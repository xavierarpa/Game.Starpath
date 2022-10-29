using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MMA;
using TMPro;
using X;
using X.Common;

public class UI_Setup : Module
{

    [Header("Settings")]
    [Space]
    [SerializeField] private CanvasGroup canvasGroup = default;

    [Header("Header")]
    [Space]
    [SerializeField] private TMP_Text tmp_text_title = default;

    [Header("Nickname")]
    [Space]
    [SerializeField] private TMP_Text tmp_text_input_name_placeholder = default;
    [SerializeField] private TMP_InputField tmp_inputfield = default;
    private static string last_tmp_inputfield = default;

    [Header("Color")]
    [Space]
    [SerializeField] private Transform tr_parent_ui_color = default;
    [SerializeField] private UI_Component_Color pref_ui_color = default;
    private static string last_hex = "";


    [Header("Message")]
    [Space]
    [SerializeField] private TMP_Text tmp_text_input_message_placeholder = default;
    [SerializeField] private TMP_InputField tmp_inputfield_message = default;

    [Header("Icon")]
    [Space]
    [SerializeField] private Transform tr_parent_ui_icon = default;
    [SerializeField] private UI_Component_Icon pref_ui_icon = default;
    private static int last_icon = 0;

    [Header("Bottom")]
    [Space]
    [SerializeField] private Button btn_continue = default;
    [SerializeField] private TMP_Text tmp_text_button_continue = default;

    private void Awake()
    {
        tmp_inputfield.text = last_tmp_inputfield;
    }
    private void Start()
    {
        OnTranslateUI();
    }

    private void Update()
    {

        if (tmp_inputfield.text.Length>10)
        {
            tmp_inputfield.text = tmp_inputfield.text.Substring(0, 9);
        }


        if (tmp_inputfield_message.text.Length > 10)
        {
            tmp_inputfield_message.text = tmp_inputfield_message.text.Substring(0, 140);
        }

    }

    protected override void OnSubscription(bool condition)
    {
        //OnTranslateUI
        //Middleware.Subscribe_Publish(condition, MMA.Localization.Key.OnSetStringTable, OnTranslateUI);
    }


    private void OnTranslateUI()
    {
        tmp_text_title.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_title);
        tmp_text_input_name_placeholder.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_input_name_placeholder);
        tmp_text_input_message_placeholder.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_input_message_placeholder);
        tmp_text_button_continue.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_continue);
    }

    private void clearCOlors()
    {
        this.tr_parent_ui_color.ClearChilds();
    }

    private void clearIcons()
    {
        this.tr_parent_ui_icon.ClearChilds();
    }

    public void SetColors(List<string> list)
    {
        clearCOlors();

        for (int i = 0; i < list.Count; i++)
        {
            var _ui_color = Instantiate(pref_ui_color, Vector3.zero,Quaternion.identity, tr_parent_ui_color);
            _ui_color.Set(list[i]);
            if (i == 0) _ui_color.OnSelected();
        }


        if (last_hex.Length>0)
        {
            Middleware<string>.Invoke_Publish("Color_Selected", last_hex);
        }

    }

    public void SetIcons()
    {
        clearIcons();

        var _list = GameManager._.list_character_sprites;

        for (int i = 0; i < _list.Count; i++)
        {
            var _instance = Instantiate(pref_ui_icon, Vector3.zero, Quaternion.identity, tr_parent_ui_icon);
            _instance.Set(i);
            if (i == 0) _instance.OnSelected();
        }

        Middleware<int>.Invoke_Publish("Icon_Selected", last_icon);
        
    }



    public void Status(bool condition)
    {
        gameObject.SetActive(condition);
        canvasGroup.alpha = condition ? 1 : 0;
        canvasGroup.interactable = condition;
        canvasGroup.blocksRaycasts = condition;
    }


    //el boton llama a esto
    public void OnSubmit()
    {

        tr_parent_ui_icon.Components(out UI_Component_Icon[] ui_icons);
        var ui_icon = ui_icons.FirstOrDefault(_i => _i.img_selected.enabled);

        tr_parent_ui_color.Components(out UI_Component_Color[] ui_colors);
        UI_Component_Color ui_color_selected = default;
        for (int i = 0; i < ui_colors.Length; i++)
        {
            if (ui_colors[i].img_selected.enabled)
            {
                ui_color_selected = ui_colors[i];
            }
        }

        if (ui_color_selected is null)
        {
            Debug.LogWarning("No ha elegido color dios");
            return;
        }

        if (ui_icon is null)
        {
            Debug.LogWarning("no hay seleccionado la forma");
            return;
        }

        if (GameManager.Validate_Fingersprint_Message(tmp_inputfield_message.text))
        {
            //OK INPUT MSG

            if (GameManager.Validate_Fingersprint_Nickname(tmp_inputfield.text)) 
            {
                //OK INPUT

                if (GameManager.Validate_Fingersprint_Color(ui_color_selected.hex))
                {
                    //OK COLOR SELECTION

                    //OK INDEX ICON
                    GameManager._.Set_Fingersprint_IconByIndex(ui_icon.index);

                    Debug.Log($"SETUP_READY: nick:'{tmp_inputfield.text}' color: '{ui_color_selected.hex}'");
                    Middleware.Invoke_Publish("SETUP_READY");
                }
                else
                {
                    Debug.LogWarning("Formulario err Color");
                }
            }
            else
            {
                Debug.LogWarning("Formulario err input");
            }
        }
        else
        {
            Debug.LogWarning("Err Mensaje");
        }
    }
}
