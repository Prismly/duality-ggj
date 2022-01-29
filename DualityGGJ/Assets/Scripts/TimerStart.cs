using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) {
        GameEvents.current.timerStartTrigger();
    }
}
