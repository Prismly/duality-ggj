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

    protected KeyCode leftKey = KeyCode.LeftArrow;
    protected KeyCode rightKey = KeyCode.RightArrow;
    protected KeyCode jumpKey = KeyCode.UpArrow;
    protected KeyCode transformKey = KeyCode.Space;

    protected static CameraController cameraController;
    
    [SerializeField]
    protected Animator anim;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputChecks();
    }

    void InputChecks()
    {
        anim = gameObject.GetComponent<Animator>();
        
        //HORIZONTAL MOVEMENT
        float xVel = rigidbody.velocity.x;

        if (Input.GetKey(leftKey))
        {
            xVel -= accelFactor * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("KeyDown", true);
        }
        if (Input.GetKey(rightKey))
        {
            xVel += accelFactor * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("KeyDown", true);
        }

        if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            if (xVel > 0.01f)
                xVel -= decelFactor * Time.deltaTime;
            if (xVel < -0.01f)
                xVel += decelFactor * Time.deltaTime;
                anim.SetBool("KeyDown", false);
        }

        xVel = Mathf.Clamp(xVel, -speedCap, speedCap);
        rigidbody.velocity = new Vector2(xVel, rigidbody.velocity.y);


        //VERTICAL MOVEMENT
        if (!isAirborne && Input.GetKeyDown(jumpKey))
        {
            rigidbody.velocity += Vector2.up * jumpVel;
            isAirborne = true;
        }

        if (rigidbody.velocity.y > 0)
            rigidbody.gravityScale = lowGrav;
        else
            rigidbody.gravityScale = highGrav;


        //TRANSFORMATION
        TransformCheck();
    }

    public virtual void TransformCheck()
    {
        //Implemented in child classes
    }
}
