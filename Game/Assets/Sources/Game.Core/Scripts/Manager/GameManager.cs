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

    [Header("DATABASE")]
    [Space]
    public List<Sprite> list_character_sprites = new List<Sprite>();
         


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

        //Cargamos el MAPA
        yield return ServiceManager._.HandleWorld();

        //QUITAMOS UI
        UIManager._.ui_loading.Status(false);
        UIManager._.ui_game.Status(true);

        //JUGADOR CON INPUTS OK
        Character._.canInputs = true;

        yield return null;
    }


    public static bool Validate_Fingersprint_Nickname(in string k)
    {
        if (k.Length>=0 && k.Length<11)
        {
            _.player_fingerprint.Nick = k;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Validate_Fingersprint_Message(in string k)
    {
        if (k.Length >= 0 && k.Length < 141)
        {
            _.player_fingerprint.Message = k;
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


    public void Set_Fingersprint_IconByIndex(int index)
    {
        player_fingerprint.SpriteIndex = index;
    }

    public IEnumerator Initialize()
    {

        yield return null;
    }


    public IEnumerator SubmitPlayer()
    {
        player_fingerprint.Position_X = Character._.transform.position.x;
        player_fingerprint.Position_Y = Character._.transform.position.z;
        player_fingerprint.CreatedAt = System.DateTime.Now;
        player_fingerprint.SpriteIndex = Character._.indexSpr;
        yield return MMA.Firebase_Firestore.Service.Set((Fingerprint.RANDOM_DOCUMENT, player_fingerprint)).ToCoroutine();
        yield return MMA.Firebase_Firestore.Service.Set((Data_Firebase.Document.LAST_FINGERPRINT, player_fingerprint)).ToCoroutine();
        yield return null;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
