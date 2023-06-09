using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private bool isZoomed = false;
    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = GameObject.FindWithTag("Player");

        if(Input.GetKeyDown(KeyCode.Z))
        {
            isZoomed = !isZoomed;
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            isFollowing = !isFollowing;
        }

        if (isFollowing)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        else
            transform.position = new Vector3(0, 0, -10);

        if (isZoomed)
            gameObject.GetComponent<Camera>().orthographicSize = 10;
        else
            gameObject.GetComponent<Camera>().orthographicSize = 5;
    }
}
