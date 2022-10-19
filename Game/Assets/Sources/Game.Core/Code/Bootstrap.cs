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
        yield return SceneManager.LoadSceneAsync("MMA.Scene", LoadSceneMode.Additive);

        //Cargamos manualmente Scene

        //Revisaremos tu sistema operativo para designar el idioma
        //TODO: U


        yield return null;
    }

}
