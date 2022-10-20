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

        yield return null;
    }
}
