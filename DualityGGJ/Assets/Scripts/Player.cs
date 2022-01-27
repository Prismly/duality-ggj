using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    KeyCode left = KeyCode.LeftArrow;
    KeyCode right = KeyCode.RightArrow;
    float pSpeed = 5f;

    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horVel = 0;
        if (Input.GetKey(left))
            horVel -= pSpeed;
        if (Input.GetKey(right))
            horVel += pSpeed;
        myRigidbody.velocity = new Vector3(horVel, myRigidbody.velocity.y, 0); ;
    }
}
