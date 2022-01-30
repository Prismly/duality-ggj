using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    GameObject timeKeeperObject, backButton, levelComplete, gold, silver, bronze;
    TimeKeeper timeKeeper;
    bool triggered = false;

    private void Start()
    {
        timeKeeper = timeKeeperObject.GetComponent<TimeKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            triggered = true;
            timeKeeper.endLevelTimer();

            if (timeKeeper.timeInSeconds < PlayerPrefs.GetFloat(timeKeeper.levelKey))
            {
                PlayerPrefs.SetFloat(timeKeeper.levelKey, timeKeeper.timeInSeconds);
            }

            levelComplete.GetComponent<Image>().enabled = true;
            if(timeKeeper.timeInSeconds < timeKeeper.goldTime)
            {
                gold.GetComponent<Image>().enabled = true;
            }
            if (timeKeeper.timeInSeconds < timeKeeper.silverTime)
            {
                silver.GetComponent<Image>().enabled = true;
            }
            if (timeKeeper.timeInSeconds < timeKeeper.bronzeTime)
            {
                bronze.GetComponent<Image>().enabled = true;
            }

            backButton.GetComponent<Image>().enabled = true;
            backButton.GetComponent<Button>().enabled = true;
        }
    }
}
