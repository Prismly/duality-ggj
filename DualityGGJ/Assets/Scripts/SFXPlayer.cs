using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource explode;
    public AudioSource jump;
    public AudioSource button;

    public void PlayExplode() {
        explode.Play();
    }

    public void PlayJump() {
        jump.Play();
    }

    public void PlayButton() {
        button.Play();
    }
}
