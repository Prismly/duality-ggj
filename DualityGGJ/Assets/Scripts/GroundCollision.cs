using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{

    [SerializeField]
    GameObject Player;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            //Debug.Log("landed");
            Player.GetComponent<Player>().isAirborne = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("left the ground");
            Player.GetComponent<Player>().isAirborne = true;
        }
    }
}
