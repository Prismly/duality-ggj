using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMenu : MonoBehaviour
{

    public AudioSource button;

    public void PlayButton() {
        button.Play();
    }
}
