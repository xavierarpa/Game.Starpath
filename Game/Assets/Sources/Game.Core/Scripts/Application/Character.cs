using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;

public class Character : Application<Character.Reference, Character.Model>
{

    public Character _ = default;

    public struct Model{}
    public struct Reference{}


    
    public SpriteRenderer spr_container;
    public SpriteRenderer spr_color;

    public bool canInputs = default;

    private void Awake()
    {
        this.Singleton(ref _);
    }

    protected override void OnSubscription(bool condition)
    {
        Middleware.Subscribe_Publish(condition, "SETUP_READY", SETUP_READY);
    }

    public void SETUP_READY()
    {
        //Habilitamos los controles
        spr_color.color = GameManager._.color_player;
        name = GameManager._.player_fingerprint.Nick;
    }


    


    private void Update()
    {
        if (canInputs)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //ARRIBA
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //ABAJO
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // IZQUIERDA
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // DERECHA
            }

            if (Input.GetKey(KeyCode.Space))
            {
                // FIRMAR

                //if canMarkHere
                if (true)
                {
                    Debug.Log("Marcando");
                }
            }

        }

    }


}
