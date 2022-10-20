using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.sceneCount > 1) SceneManager.LoadScene(0);
    }
    private IEnumerator Start()
    {

        //INIT

        //Cargamos manualmente Scene
        yield return SceneManager.LoadSceneAsync("MMA.Scenes", LoadSceneMode.Additive);


        //## Localization Module
        yield return Service.Scene.Add(Data_Scenes.MMA_Localization);
        yield return Middleware<string, bool>.Invoke_Task(MMA.Localization.Key.Set_StringTable, Data_Localization.Path_Locale).ToCoroutine();

        //# Internet
        yield return Service.Scene.Add("MMA.InternetStatus");
        MMA.InternetStatus.Service.Connect((Data_InternetStatus.URL_FIREBASE, true));

        //## Firebase
        yield return Service.Scene.Add("MMA.Firebase");
        yield return Service.Scene.Add("MMA.Firebase.Firestore");
        var operation_init_firebase = Middleware<bool>.Invoke_Task(MMA.Firebase.Key.Initialize).ToCoroutine();
        yield return operation_init_firebase;




        yield return null;
    }

}

