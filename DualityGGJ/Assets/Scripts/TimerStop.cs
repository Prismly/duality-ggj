using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStop : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) {
        GameEvents.current.timerStopTrigger();
    }
}
