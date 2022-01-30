using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    GameObject timeKeeperObject;
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
            timeKeeper.startLevelTimer();
        }
    }
}
