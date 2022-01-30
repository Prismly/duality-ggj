using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField]
    GameObject sceneController;
    [SerializeField]
    string thisScenesName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sceneController.GetComponent<SceneChanger>().LoadScene(thisScenesName);
        }
    }
}
