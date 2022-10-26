using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;

public class Character : Application<Character.Reference, Character.Model>
{

    public static Character _ = default;

    public struct Model{}
    public struct Reference{}

    public const float DISTANCE_INSTANCE = 10000f;
    
    public SpriteRenderer spr_container;
    public SpriteRenderer spr_color;
    public float velocity = 10;
    public LineRenderer lineRenderer;
    public bool canInputs = default;
    public int indexSpr = 0;
    [Header("Etc")]
    [Space]
    public Mark lastMark = default;

    private float speedRotation = 0;

    public TMPro.TMP_Text text_message = default;
    public TMPro.TMP_Text text_nickname = default;
    public TMPro.TMP_Text text_createdAt = default;
    public TMPro.TMP_Text text_create = default;


    private void Awake()
    {
        this.Singleton(ref _);
        //transform.position = new Vector3((DISTANCE_INSTANCE).ZeroMax(), transform.position.y, (DISTANCE_INSTANCE).ZeroMax());
        speedRotation = 5.MinusMax();
    }

    protected override void OnSubscription(bool condition)
    {
        Middleware.Subscribe_Publish(condition, "SETUP_READY", SETUP_READY);
    }

    public void SETUP_READY()
    {
        //Habilitamos los controles
        spr_color.color = GameManager._.color_player;
        indexSpr = GameManager._.player_fingerprint.SpriteIndex;
        spr_color.sprite = GameManager._.list_character_sprites[GameManager._.player_fingerprint.SpriteIndex];
        spr_container.sprite = GameManager._.list_character_sprites[GameManager._.player_fingerprint.SpriteIndex];

#if UNITY_EDITOR
        name = GameManager._.player_fingerprint.Nick;
#endif
    }


    


    private void Update()
    {

        bool isMoveing = false;


        if (canInputs)
        {
            

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //ARRIBA
                transform.position += Vector3.forward * velocity * Time.deltaTime;
                isMoveing = true;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //ABAJO
                transform.position += Vector3.back * velocity * Time.deltaTime;
                isMoveing = true;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // IZQUIERDA
                transform.position += Vector3.left * velocity * Time.deltaTime;
                isMoveing = true;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // DERECHA
                transform.position += Vector3.right * velocity * Time.deltaTime;
                isMoveing = true;
            }


            if (isMoveing) transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);


            if (Input.GetKey(KeyCode.Space))
            {
                // FIRMAR

                //if canMarkHere
                if (true)
                {
                    canInputs = false;
                    StartCoroutine(GameManager._.SubmitPlayer());
                }
            }

        }


        if (lastMark is null)
        {
            lineRenderer.startColor = spr_color.color;
            lineRenderer.endColor = spr_color.color;
            lineRenderer.SetPosition(0, transform.position + Vector3.down);
            lineRenderer.SetPosition(1, transform.position + Vector3.down);

            text_nickname.text = "";
            text_createdAt.text = "";
            text_message.text = "";
            text_create.text = MMA.Localization.Service.Translate(Data_Localization.Key.text_create);
            
        }
        else
        {
            lineRenderer.startColor = spr_color.color;
            lineRenderer.endColor = lastMark.spr_color.color;
            lineRenderer.SetPosition(0, transform.position + Vector3.down);
            lineRenderer.SetPosition(1, lastMark.transform.position + Vector3.down);

            text_nickname.text = lastMark.fingerprint.Nick;
            text_createdAt.text = lastMark.fingerprint.CreatedAt.ToString("G");
            text_message.text = lastMark.fingerprint.Message;
        }


        if (Time.frameCount % 20 == 0)
        {
            var list_marks = FingerPrintService._ is null ? new List<Mark>() : FingerPrintService._.list_marks;
            if (list_marks.Count>0)
            {
                var _index = 0;
                lastMark = list_marks[0];
                for (int i = 0; i < list_marks.Count; i++)
                {
                    if (Vector3.Distance(lastMark.transform.position, transform.position) > Vector3.Distance(list_marks[i].transform.position, transform.position))
                    {
                        lastMark = list_marks[i];
                        _index = i;
                    }
                }

                if (GameManager._ != null)
                {
                    GameManager._.player_fingerprint.RefID = FingerPrintService._.list_id_marks[_index];
                }
            }
        }
    }


}
