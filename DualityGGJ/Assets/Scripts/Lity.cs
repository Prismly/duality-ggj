using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lity : Player
{
    [SerializeField]
    GameObject ballForm;
    static bool canDoubleJump = true;
    [SerializeField]
    float wallJumpReduction;

    private void Start()
    {
        spriteObject.GetComponent<Animator>().SetBool("Can Double Jump", canDoubleJump);
    }

    public override void JumpCheck(ref float xVel, ref float yVel)
    {
        Debug.Log(spriteObject.GetComponent<Animator>().GetBool("Can Double Jump"));

        if (!isAirborne)
        {
            spriteObject.GetComponent<Animator>().SetBool("Can Double Jump", true);
            canDoubleJump = true;
        }

        if ((!isAirborne || (isAirborne && isWallborne != WallState.NOT_WALLBORNE)) && Input.GetKeyDown(jumpKey))
        {
            //Player is either grounded OR on a wall and jumps
            spriteObject.GetComponent<Animator>().SetTrigger("Jumps");
            jump.Play();
            float jumpPower = jumpVel;
            if (isAirborne && isWallborne != WallState.NOT_WALLBORNE)
            {
                jumpPower *= wallJumpReduction;
            }

            yVel = jumpPower;
            isAirborne = true;
        }
        else if (canDoubleJump && Input.GetKeyDown(jumpKey))
        {
            //Player isn't able to jump normally and must double jump, but can
            yVel = jumpVel;
            isAirborne = true;
            spriteObject.GetComponent<Animator>().SetTrigger("Double Jumps");
            jump.Play();
            canDoubleJump = false;
        }

        if (isWallborne != WallState.NOT_WALLBORNE && Input.GetKeyDown(jumpKey))
        {
            if (isWallborne == WallState.WALLBORNE_L)
            {
                Debug.Log("jompright");
                xVel = wallJumpVel;
            }
            else
            {
                Debug.Log("jompleft");
                xVel = -wallJumpVel;
            }
            isWallborne = WallState.NOT_WALLBORNE;
        }
    }

    public override void TransformCheck()
    {
        if (Input.GetKeyDown(transformKey))
        {
            GameObject newForm = Instantiate(ballForm);
            newForm.transform.position = transform.position;
            cameraController.player = newForm.transform;
            newForm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            newForm.GetComponent<Ball>().isDua = true;
            Destroy(gameObject);
        }
    }
}
