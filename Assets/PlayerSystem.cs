using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    private GameObject player;
    public void OnStart()
    {
        player = Instantiate (Resources.Load ("Entities/Player") as GameObject, new Vector3(0, 6, 0), Quaternion.identity);
        player.GetComponent<Player>().OnStart();
    }

    public void OnUpdate()
    {
        player.GetComponent<Player>().OnUpdate();
    }
}
