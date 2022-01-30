using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string scene) {
        Debug.Log("Change scene");
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void doExitGame() {
        Application.Quit();
    }
}
