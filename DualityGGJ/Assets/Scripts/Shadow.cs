using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    public float secondsToSelfDestruct;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > secondsToSelfDestruct)
        {
            Destroy(gameObject);
        }
    }
}
