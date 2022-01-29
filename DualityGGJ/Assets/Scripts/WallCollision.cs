using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    bool isRight;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && player.GetComponent<Player>().isAirborne == true)
        {
            Debug.Log("stick");
            player.GetComponent<Player>().isWallborne = isRight ? Player.WallState.WALLBORNE_R : Player.WallState.WALLBORNE_L;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && player.GetComponent<Player>().isAirborne == true)
        {
            Debug.Log("unstick");
            player.GetComponent<Player>().isWallborne = Player.WallState.NOT_WALLBORNE;
        }
    }
}
