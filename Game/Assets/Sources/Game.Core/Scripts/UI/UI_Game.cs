using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Game : Module
{

    [Header("Settings")]
    [Space]
    [SerializeField] private CanvasGroup canvasGroup = default;


    protected override void OnSubscription(bool condition)
    {
        
    }

    public void Status(bool condition)
    {
        gameObject.SetActive(condition);
        canvasGroup.alpha = condition ? 1 : 0;
        canvasGroup.interactable = condition;
        canvasGroup.blocksRaycasts = condition;
    }
}
