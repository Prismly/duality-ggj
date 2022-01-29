using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    public float wallJumpVel, jumpVel, upGrav, downGrav, wallDownGrav;
    [SerializeField]
    public float speedCap, accelFactor, decelFactor;
    [SerializeField]
    public GameObject spriteObject;

    public bool isAirborne = true;
    public WallState isWallborne = WallState.NOT_WALLBORNE;
    public enum WallState
    {
        NOT_WALLBORNE,
        WALLBORNE_L,
        WALLBORNE_R
    }

    protected KeyCode leftKey = KeyCode.LeftArrow;
    protected KeyCode rightKey = KeyCode.RightArrow;
    protected KeyCode jumpKey = KeyCode.UpArrow;
    protected KeyCode transformKey = KeyCode.Space;

    [SerializeField]
    protected CameraController cameraController;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputChecks();
    }

    void InputChecks()
    {
        //HORIZONTAL MOVEMENT
        float xVel = rigidbody.velocity.x;

        if (Input.GetKey(leftKey))
        {
            xVel -= accelFactor * Time.deltaTime;
            spriteObject.GetComponent<SpriteRenderer>().flipX = true;
            spriteObject.GetComponent<Animator>().SetBool("KeyDown", true);
        }
        if (Input.GetKey(rightKey))
        {
            xVel += accelFactor * Time.deltaTime;
            spriteObject.GetComponent<SpriteRenderer>().flipX = false;
            spriteObject.GetComponent<Animator>().SetBool("KeyDown", true);
        }
        if(!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            spriteObject.GetComponent<Animator>().SetBool("KeyDown", false);
        }

        if (!isAirborne && !Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            if (xVel > 0.01f)
                xVel -= decelFactor * Time.deltaTime;
            if (xVel < -0.01f)
                xVel += decelFactor * Time.deltaTime;
        }

        xVel = Mathf.Clamp(xVel, -speedCap, speedCap);
        rigidbody.velocity = new Vector2(xVel, rigidbody.velocity.y);


        //VERTICAL MOVEMENT
        if ((!isAirborne || isWallborne != WallState.NOT_WALLBORNE) && Input.GetKeyDown(jumpKey))
        {
            //Player is either grounded OR on a wall and jumps
            rigidbody.velocity += Vector2.up * jumpVel;
            isAirborne = true;
        }

        if (isWallborne != WallState.NOT_WALLBORNE && Input.GetKeyDown(jumpKey))
        {
            if (isWallborne == WallState.WALLBORNE_L)
            {
                Debug.Log("jompright");
                rigidbody.velocity += Vector2.right * wallJumpVel;
            }
            else
            {
                Debug.Log("jompleft");
                rigidbody.velocity += Vector2.left * wallJumpVel;
            }
            isWallborne = WallState.NOT_WALLBORNE;
        }

        if (rigidbody.velocity.y > 0)
            rigidbody.gravityScale = upGrav;
        else if (isWallborne == WallState.NOT_WALLBORNE)
            rigidbody.gravityScale = downGrav;
        else
            rigidbody.gravityScale = wallDownGrav;


        //TRANSFORMATION
        TransformCheck();
    }

    public virtual void TransformCheck()
    {
        //Implemented in child classes
    }
}
