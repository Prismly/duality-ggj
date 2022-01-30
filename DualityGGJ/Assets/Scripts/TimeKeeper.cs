using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    bool timerRunning = false;
    [SerializeField]
    public string levelKey;
    public float timeInSeconds;
    [SerializeField]
    public float goldTime, silverTime, bronzeTime;
    [SerializeField]
    public GameObject visualTimerUI, sceneController;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(levelKey))
        {
            PlayerPrefs.SetFloat(levelKey, float.MaxValue);
        }
    }

    public void startLevelTimer()
    {
        Debug.Log("Timer Started");
        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning)
        {
            timeInSeconds += Time.deltaTime;
            visualTimerUI.GetComponent<Text>().text = secToString(timeInSeconds);
        }
    }

    public string secToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time % 1) * 1000);

        return string.Format("{0:D2}:{1:D2}.{2:D3}", minutes, seconds, milliseconds);
    }

    public void endLevelTimer()
    {
        timerRunning = false;
    }
}
