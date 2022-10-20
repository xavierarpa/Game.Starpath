using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Loading : Module
{
    [Header("Settings")]
    [Space]
    [SerializeField] private CanvasGroup canvasGroup = default;
    [SerializeField] private TMP_Text tmp_text_message = default;


    private void Start()
    {
        OnTranslateUI();
    }

    protected override void OnSubscription(bool condition)
    {
        //OnTranslateUI
        Middleware.Subscribe_Publish(condition, MMA.Localization.Key.OnSetStringTable, OnTranslateUI);
    }


    private void OnTranslateUI()
    {
        tmp_text_message.text = MMA.Localization.Service.Translate(Data_Localization.Key.loading_message);
    }


    public void Status(bool condition)
    {
        gameObject.SetActive(condition);
        canvasGroup.alpha = condition ? 1 : 0;
        canvasGroup.interactable = condition;
        canvasGroup.blocksRaycasts = condition;
    }
}
