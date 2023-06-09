using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    PLAYING, FROZEN, MENU
}

public class GameManager : MonoBehaviour
{
    public PlayerSystem playerSystem;
    public EnemySystem enemySystem;
    public CustomEventSystem eventSystem;
    public GameState gameState;

    void Start()
    {
        gameState = GameState.PLAYING;
        playerSystem.OnStart();
    }

    void Update()
    {
        if(gameState == GameState.PLAYING)
            playerSystem.OnUpdate();

        if(Input.GetKeyDown(KeyCode.I))
        {
            SetIngameState(GameState.FROZEN);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            SetIngameState(GameState.PLAYING);
        }
    }

    public void SetIngameState(GameState state)
    {
        gameState = state;
    }

    public void SendEvent(CustomEvent e)
    {
        eventSystem.SendEvent(e);
    }
}
