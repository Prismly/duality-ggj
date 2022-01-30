using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float wallJumpVel, jumpVel, upGrav, downGrav, wallDownGrav;
    [SerializeField]
    public float speedCapX, speedCapY, accelFactor, decelFactor;
    [SerializeField]
    public GameObject spriteObject;
    [SerializeField]
    public float maxSpeedAtWhichWallsCanBeGrabbed;

    Rigidbody2D rigidbody;

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

    [SerializeField]
    protected AudioSource jump;
    [SerializeField]
    public AudioSource explode;

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
        float xVel = GetComponent<Rigidbody2D>().velocity.x;

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
        float yVel = GetComponent<Rigidbody2D>().velocity.y;

        JumpCheck(ref xVel, ref yVel);

        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = upGrav;
        }
        else if (isWallborne == WallState.NOT_WALLBORNE)
        {
            GetComponent<Rigidbody2D>().gravityScale = downGrav;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = wallDownGrav;
        }

        xVel = Mathf.Clamp(xVel, -speedCapX, speedCapX);
        yVel = Mathf.Clamp(yVel, -speedCapY, speedCapY);
        //Debug.Log(yVel);
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);

        spriteObject.GetComponent<Animator>().SetFloat("Vert Velocity", GetComponent<Rigidbody2D>().velocity.y);

        //TRANSFORMATION
        TransformCheck();
    }

    public virtual void JumpCheck(ref float xVel, ref float yVel)
    {
        if (!isAirborne && isWallborne == WallState.NOT_WALLBORNE && Input.GetKeyDown(jumpKey))
        {
            //Player is grounded AND not on a wall and jumps (for Dua mostly)
            spriteObject.GetComponent<Animator>().SetTrigger("Jumps");
            jump.Play();
            yVel = jumpVel;
            isAirborne = true;
        }
    }

    public virtual void TransformCheck()
    {
        //Implemented in child classes
    }
}
