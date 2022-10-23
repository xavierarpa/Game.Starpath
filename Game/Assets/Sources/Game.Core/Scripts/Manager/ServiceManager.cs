using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X.Common;
using X;

public class ServiceManager : MonoBehaviour
{
    public static ServiceManager _ = default;

    [Header("Settings")]
    [Space]
    public SettingsService service_settings = default;
    public FingerPrintService service_fingerprint = default;

    private void Awake()
    {
        this.Singleton(ref _);
    }

    public IEnumerator Initialize()
    {

        //Initialize Settings
        //Debug.Log("Get Colors");
        yield return service_settings.Initialize().ToCoroutine();

        yield return null;
    }


    public IEnumerator HandleWorld() => service_fingerprint.Get_FingerPrints();
}
