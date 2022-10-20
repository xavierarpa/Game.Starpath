using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField] private UI_Setup ui_setup = default;
    [SerializeField] private UI_Loading ui_loading = default;

    private void Awake()
    {
        ui_loading.Status(true);
        ui_setup.Status(false);
    }


    public IEnumerator Initialize()
    {

        //Activamos el Setup cuando confirmamos que se puede jugar
        ui_loading.Status(false);
        ui_setup.Status(true);

        yield return null;
    }
}
