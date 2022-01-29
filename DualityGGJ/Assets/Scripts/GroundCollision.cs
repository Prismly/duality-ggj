using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            //Debug.Log("landed");
            player.GetComponent<Player>().isAirborne = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("left the ground");
            player.GetComponent<Player>().isAirborne = true;
        }
    }
}
