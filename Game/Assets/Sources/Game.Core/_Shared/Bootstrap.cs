using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [Header("Managers")]
    [Space]
    [SerializeField] private GameManager manager_game = default;
    [SerializeField] private UIManager manager_ui = default;
    [SerializeField] private ServiceManager manager_service = default;

    [Header("Services")]
    [SerializeField] private ZoneService service_zone = default;
    [SerializeField] private SettingsService service_settings = default;

    private void Awake()
    {
        if (SceneManager.sceneCount > 1) SceneManager.LoadScene(0);
    }

    private IEnumerator Start()
    {

        //INIT
        Debug.Log("---- INIT ----");

        //Cargamos manualmente Scene
        yield return SceneManager.LoadSceneAsync("MMA.Scenes", LoadSceneMode.Additive);


        //## Localization Module
        yield return Service.Scene.Add("MMA.Localization");
        yield return Middleware<string, bool>.Invoke_Task(MMA.Localization.Key.Set_StringTable, Data_Localization.Path_Locale).ToCoroutine();
        //var a = MMA.Localization.Service.Translate("common_lang");
        //Debug.Log(a);


        //# Internet
        yield return Service.Scene.Add("MMA.InternetStatus");
        MMA.InternetStatus.Service.Connect((Data_InternetStatus.URL_FIREBASE, true));

        //## Firebase
        yield return Service.Scene.Add("MMA.Firebase");
        yield return Service.Scene.Add("MMA.Firebase.Firestore");
        yield return Middleware<bool>.Invoke_Task(MMA.Firebase.Key.Initialize).ToCoroutine();
        MMA.Firebase_Firestore.Service.Initialize();

        //## ServiceManager
        yield return manager_service.Initialize();


        //## UIManager
        yield return manager_ui.Initialize();


        //## GameManager
        yield return manager_game.Initialize();

        //END
        Debug.Log("---- END ----");
    }
}
