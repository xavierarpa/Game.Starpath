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

    [Header("Color")]
    [Space]
    [SerializeField] private Transform tr_parent_ui_color = default;
    [SerializeField] private UI_Component_Color pref_ui_color = default;

    [Header("Bottom")]
    [Space]
    [SerializeField] private Button btn_continue = default;
    [SerializeField] private TMP_Text tmp_text_button_continue = default;

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
        
    }

    protected override void OnSubscription(bool condition)
    {
        //OnTranslateUI
        Middleware.Subscribe_Publish(condition, MMA.Localization.Key.OnSetStringTable, OnTranslateUI);
    }


    private void OnTranslateUI()
    {
        tmp_text_title.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_title);
        tmp_text_input_name_placeholder.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_input_name_placeholder);
        tmp_text_button_continue.text = MMA.Localization.Service.Translate(Data_Localization.Key.setup_continue);
    }

    private void clearCOlors()
    {
        this.tr_parent_ui_color.ClearChilds();
    }

    public void SetColors(List<string> list)
    {
        clearCOlors();

        for (int i = 0; i < list.Count; i++)
        {
            var _ui_color = Instantiate(pref_ui_color, Vector3.zero,Quaternion.identity, tr_parent_ui_color);
            _ui_color.Set(list[i]);
        }

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

        if (GameManager.Validate_Fingersprint_Nickname(tmp_inputfield.text)) 
        {
            //OK INPUT

            if (GameManager.Validate_Fingersprint_Color(ui_color_selected.hex))
            {
                //OK COLOR SELECTION
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
}
