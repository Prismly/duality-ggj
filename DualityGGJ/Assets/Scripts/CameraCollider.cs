using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField]
    CameraController camera;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "CameraLimit")
        {
            Debug.Log("out of limit");
            camera.inLimit = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraLimit")
        {
            camera.inLimit = true;
        }
    }
}
