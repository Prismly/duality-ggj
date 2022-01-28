using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    public float jumpVel, lowGrav, highGrav;
    [SerializeField]
    public float speedCap, accelFactor, decelFactor;

    public bool isAirborne = true;

    KeyCode left = KeyCode.LeftArrow;
    KeyCode right = KeyCode.RightArrow;
    KeyCode jump = KeyCode.UpArrow;
    KeyCode transform = KeyCode.DownArrow;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorMovement();
        VerMovement();
        Transformation();
    }

    void HorMovement()
    {
        float xVel = rigidbody.velocity.x;

        if (Input.GetKey(left))
        {
            xVel -= accelFactor * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(right))
        {
            xVel += accelFactor * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (!Input.GetKey(left) && !Input.GetKey(right))
        {
            if (xVel > 0.01f)
                xVel -= decelFactor * Time.deltaTime;
            if (xVel < -0.01f)
                xVel += decelFactor * Time.deltaTime;
        }

        xVel = Mathf.Clamp(xVel, -speedCap, speedCap);

        //Debug.Log(xVel);
        rigidbody.velocity = new Vector2(xVel, rigidbody.velocity.y);
    }

    void VerMovement()
    {
        if (!isAirborne && Input.GetKeyDown(jump))
        {
            rigidbody.velocity += Vector2.up * jumpVel;
            isAirborne = true;
        }

        if (rigidbody.velocity.y > 0)
            rigidbody.gravityScale = lowGrav;
        else
            rigidbody.gravityScale = highGrav;
    }

    public void Transformation()
    {
        if (Input.GetKeyDown(transform))
        {
            //CharacterManager.TransformCharacter(this);
        }
        if (Input.GetKeyUp(transform))
        {

        }
    }
}
