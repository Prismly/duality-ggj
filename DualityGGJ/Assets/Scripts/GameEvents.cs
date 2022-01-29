using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action timerStart;
    public event Action timerStop;

    public void timerStartTrigger() {
        if (timerStart != null) {
            timerStart();
        }
    }

    public void timerStopTrigger() {
        if (timerStop != null) {
            timerStop();
        }
    }
}
