using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTimes : MonoBehaviour
{
    [SerializeField] public static float TutBestTime = 300;
    [SerializeField] public static float L1BestTime = 300;
    [SerializeField] public static float L2BestTime = 300;
    [SerializeField] public static float L3BestTime = 300;

    [SerializeField] public bool playingTut = false;
    [SerializeField] public bool playingL1 = false;
    [SerializeField] public bool playingL2 = false;
    [SerializeField] public bool playingL3 = false;

    public void checkIfBestTime(float currentTime) {
        if(playingTut) {
            if(currentTime < TutBestTime) {
                TutBestTime = currentTime;
            }
        }
        else if(playingL1) {
            if(currentTime < L1BestTime) {
                L1BestTime = currentTime;
            }
        }
        else if(playingL2) {
            if(currentTime < L2BestTime) {
                L2BestTime = currentTime;
            }
        }
        else if(playingL3) {
            if(currentTime < L3BestTime) {
                L3BestTime = currentTime;
            }
        }
    }
}
