using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground") {
            //player.GetComponent<Player>().spriteObject.GetComponent<Animator>().SetBool("Airborne", false);
            //Debug.Log("lands" + Time.frameCount);
            player.GetComponent<Player>().spriteObject.GetComponent<Animator>().SetTrigger("Lands");
            player.GetComponent<Player>().spriteObject.GetComponent<Animator>().SetBool("Grounded", true);
            player.GetComponent<Player>().isAirborne = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //player.GetComponent<Player>().spriteObject.GetComponent<Animator>().SetBool("Airborne", true);
            player.GetComponent<Player>().spriteObject.GetComponent<Animator>().ResetTrigger("Lands");
            player.GetComponent<Player>().spriteObject.GetComponent<Animator>().SetBool("Grounded", false);
            player.GetComponent<Player>().isAirborne = true;
        }
    }
}
