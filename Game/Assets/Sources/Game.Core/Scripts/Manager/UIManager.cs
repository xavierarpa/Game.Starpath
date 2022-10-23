using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Module
{
    public static UIManager _ = default;

    [Header("Settings")]
    [Space]
    public UI_Setup ui_setup = default;
    public UI_Loading ui_loading = default;
    public UI_Game ui_game = default;

    private void Awake()
    {
        X.Common.Supply.Singleton(this, ref _);

        ui_loading.Status(true);
        ui_setup.Status(false);
        ui_game.Status(false);
    }

    protected override void OnSubscription(bool condition)
    {
        // SettingsService_On_GET_Color
        Middleware<List<string>>.Subscribe_Publish(condition, "SettingsService_On_GET_Color", SettingsService_On_GET_Color);
    }

    public IEnumerator Initialize()
    {

        yield return null;
    }


    private void SettingsService_On_GET_Color(List<string> list)
    {
        ui_loading.Status(false);
        ui_setup.Status(true);

        ui_setup.SetColors(list);
        ui_setup.SetIcons();
    }
}
