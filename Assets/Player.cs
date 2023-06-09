using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public GameManager gameManager;
    
    //Player state
    public int health;

    public void OnStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = gameObject.GetComponent<PlayerController>();
        playerController.OnStart();
    }

    public void OnUpdate()
    {
        playerController.OnUpdate();

        if(Input.GetKeyDown(KeyCode.K))
        {
            gameManager.SendEvent(new CastSpellEvent(gameObject, SpellType.DASH));
        }
    }
}
