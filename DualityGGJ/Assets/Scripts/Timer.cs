using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float time;
    public Text currentTimeText;

    public bool timerActive = false;

    [SerializeField] private Image levelComplete;
    [SerializeField] private Image gold;
    [SerializeField] private Image silver;
    [SerializeField] private Image bronze;

    [SerializeField] int goldTime;
    [SerializeField] int silverTime;
    [SerializeField] int bronzeTime;
    
    void Start()
    {
        time = 0;
        GameEvents.current.timerStart += onTimerStart;
        GameEvents.current.timerStop += onTimerStop;
    }

    private void onTimerStart() {
        timerActive = true;
    }

    private void onTimerStop() {
        timerActive = false;
        levelComplete.enabled = true;
        
        if(time < bronzeTime) {
            bronze.enabled = true;
        }
        if(time < silverTime) {
            silver.enabled = true;
        }
        if(time < goldTime) {
            gold.enabled = true;   
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(timerActive == true) {
            time += Time.deltaTime;
        }
        currentTimeText.text = secToString(time);
    }

    public string secToString(float time) {
        int minutes = (int) (time / 60);
        int seconds = (int) (time % 60);
        int milliseconds = (int) ((time % 1) * 1000);

        return minutes + ":" + seconds + "." + milliseconds;
    }
}
