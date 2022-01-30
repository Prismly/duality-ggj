using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rigidbody;

    [SerializeField]
    public float wallJumpVel, jumpVel, upGrav, downGrav, wallDownGrav;
    [SerializeField]
    public float speedCapX, speedCapY, accelFactor, decelFactor;
    [SerializeField]
    public GameObject spriteObject;

    protected bool facingRight = true;
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

    public virtual void InputChecks()
    {
        //HORIZONTAL MOVEMENT
        float xVel = rigidbody.velocity.x;

        if ((facingRight == true && isWallborne == WallState.WALLBORNE_L) || (facingRight == false && isWallborne == WallState.WALLBORNE_R))
        {
            spriteObject.GetComponent<Animator>().SetBool("Touching Wall", false);
            spriteObject.GetComponent<Animator>().SetBool("Wallborne", false);
        }

        if (!Input.GetKey(rightKey) && Input.GetKey(leftKey))
        {
            xVel -= accelFactor * Time.deltaTime;
            facingRight = false;
            spriteObject.GetComponent<SpriteRenderer>().flipX = true;
            spriteObject.GetComponent<Animator>().SetBool("Move Key Pressed", true);
        }
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            xVel += accelFactor * Time.deltaTime;
            facingRight = true;
            spriteObject.GetComponent<SpriteRenderer>().flipX = false;
            spriteObject.GetComponent<Animator>().SetBool("Move Key Pressed", true);
        }
        else
        {
            if (!isAirborne)
            {
                if (xVel > 0.01f)
                    xVel -= decelFactor * Time.deltaTime;
                if (xVel < -0.01f)
                    xVel += decelFactor * Time.deltaTime;
            }
            spriteObject.GetComponent<Animator>().SetBool("Move Key Pressed", false);
        }

        //VERTICAL MOVEMENT
        float yVel = rigidbody.velocity.y;

        JumpCheck(ref xVel, ref yVel);

        if (rigidbody.velocity.y > 0)
        {
            rigidbody.gravityScale = upGrav;
        }
        else if (isWallborne == WallState.NOT_WALLBORNE)
        {
            rigidbody.gravityScale = downGrav;
        }
        else
        {
            rigidbody.gravityScale = wallDownGrav;
        }

        xVel = Mathf.Clamp(xVel, -speedCapX, speedCapX);
        yVel = Mathf.Clamp(yVel, -speedCapY, speedCapY);
        //Debug.Log(yVel);
        rigidbody.velocity = new Vector2(xVel, yVel);
		
        //TRANSFORMATION
        TransformCheck();
    }

    public virtual void JumpCheck(ref float xVel, ref float yVel)
    {
        if (!isAirborne && isWallborne == WallState.NOT_WALLBORNE && Input.GetKeyDown(jumpKey))
        {
            //Player is either grounded OR on a wall and jumps
            yVel += jumpVel;
            isAirborne = true;
        }
    }

    //When the player holds both left and right, we want the character to respond as if the previous button was held.
    public Vector2 PickHorInputDirection()
    {
        if(Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            return facingRight ? Vector2.right : Vector2.left;
        }
        else if(Input.GetKey(leftKey))
        {
            return Vector2.left;
        }
        else if(Input.GetKey(rightKey))
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public virtual void TransformCheck()
    {
        //Implemented in child classes
    }

    public virtual void PlayJump() {
        //Implemented in child classes
    }
}
