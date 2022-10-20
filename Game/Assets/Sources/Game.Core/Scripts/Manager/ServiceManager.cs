using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField] private SettingsService service_settings = default;
    [SerializeField] private ZoneService service_zone = default;

    public IEnumerator Initialize()
    {

        //Initialize Settings
        //Debug.Log("Get Colors");
        yield return service_settings.Initialize().ToCoroutine();

        yield return null;
    }
}
