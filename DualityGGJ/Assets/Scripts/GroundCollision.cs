using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{

    [SerializeField]
    GameObject Player;

    void OnTriggerEnter2d(Collider2D collision) {
        if(collision.gameObject.tag == "Ground") {
            Debug.Log("jumpe trigger");
            Player.GetComponent<Player>().isAirborne = false;
        }
    }
}
