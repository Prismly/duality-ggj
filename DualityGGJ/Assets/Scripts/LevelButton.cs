using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    string levelKey;
    [SerializeField]
    GameObject bestTimeTextBox;

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat(levelKey));
        if (PlayerPrefs.HasKey(levelKey))
        {
            bestTimeTextBox.GetComponent<Text>().text = secToString(PlayerPrefs.GetFloat(levelKey));
        }
        else
        {
            bestTimeTextBox.GetComponent<Text>().text = "N/A";
        }
    }

    private string secToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time % 1) * 1000);

        return string.Format("{0:D2}:{1:D2}.{2:D3}", minutes, seconds, milliseconds);
    }
}
