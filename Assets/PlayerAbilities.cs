using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void FreezeTime()
    {
        gameManager.SetIngameState(GameState.FROZEN);
    }

}