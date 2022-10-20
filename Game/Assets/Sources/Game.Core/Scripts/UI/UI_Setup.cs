using System.Collections;
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
        OnSetColors();
        OnTranslateUI();
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

    private void OnSetColors()
    {
        this.tr_parent_ui_color.ClearChilds();

        //TODO llamar a DB y revisar las opciones
        //for (int i = 0; i < max; i++)
        //{

        //}
    }



    public void Status(bool condition)
    {
        gameObject.SetActive(condition);
        canvasGroup.alpha = condition ? 1 : 0;
        canvasGroup.interactable = condition;
        canvasGroup.blocksRaycasts = condition;
    }

}
