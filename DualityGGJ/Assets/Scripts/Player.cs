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

        //VERTICAL MOVEMENT
        float yVel = rigidbody.velocity.y;

        JumpCheck(ref xVel, ref yVel);

        if (rigidbody.velocity.y > 0)
            rigidbody.gravityScale = upGrav;
        else if (isWallborne == WallState.NOT_WALLBORNE)
            rigidbody.gravityScale = downGrav;
        else
            rigidbody.gravityScale = wallDownGrav;

        xVel = Mathf.Clamp(xVel, -speedCap, speedCap);
        yVel = Mathf.Clamp(yVel, -speedCap, speedCap);
        rigidbody.velocity = new Vector2(xVel, yVel);

        //TRANSFORMATION
        TransformCheck();
    }

    public virtual void JumpCheck(ref float xVel, ref float yVel)
    {
        if ((!isAirborne || isWallborne != WallState.NOT_WALLBORNE) && Input.GetKeyDown(jumpKey))
        {
            //Player is either grounded OR on a wall and jumps
            yVel += jumpVel;
            isAirborne = true;
        }

        if (isWallborne != WallState.NOT_WALLBORNE && Input.GetKeyDown(jumpKey))
        {
            if (isWallborne == WallState.WALLBORNE_L)
            {
                Debug.Log("jompright");
                xVel += wallJumpVel;
            }
            else
            {
                Debug.Log("jompleft");
                xVel -= wallJumpVel;
            }
            isWallborne = WallState.NOT_WALLBORNE;
        }
    }

    public virtual void TransformCheck()
    {
        //Implemented in child classes
    }
}
