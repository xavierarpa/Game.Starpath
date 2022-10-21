using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;


public class GameManager : Module
{

    public static GameManager _ = default;

    [Header("Settings")]
    [Space]
    public Fingerprint player_fingerprint = new Fingerprint(); // elemento que se va armando con el tiempo

    public Color color_player;

    private void Awake()
    {
        this.Singleton(ref _);
    }

    protected override void OnSubscription(bool condition)
    {
        //SETUP_READY
        Middleware.Subscribe_Publish(condition, "SETUP_READY", SETUP_READY);

        
    }

    private void SETUP_READY()
    {
        Debug.Log("///TODO Empezamos el jeugo !");
        //Terminamos el SETUP
        UIManager._.ui_setup.Status(false);

        //PONEMOS A CARGAR
        UIManager._.ui_loading.Status(true);


        //CREAMOS EL MUNDO
        StartCoroutine(BeginWorld());

    }

    private IEnumerator BeginWorld()
    {
        Debug.Log("Creating WOrld");
        //FIXME falta crear el NEXO y el monigote, y cargar la ZONA


        yield return null;
    }


    public static bool Validate_Fingersprint_Nickname(in string k)
    {
        if (k.Length>0 && k.Length<11)
        {
            _.player_fingerprint.Nick = k;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Validate_Fingersprint_Color(in string k)
    {
        if (ColorUtility.TryParseHtmlString(k, out Color color))
        {
            _.color_player = color;
            Debug.Log($"<color={k}>VALID '{k}'</color>");
            _.player_fingerprint.Color = k;
            return true;
        }
        return false;
        //ColorUtility.ToHtmlStringRGBA(myColor);
    }

    public IEnumerator Initialize()
    {

        yield return null;
    }
}
