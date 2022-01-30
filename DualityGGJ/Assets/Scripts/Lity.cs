using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lity : Player
{
    [SerializeField]
    GameObject ballForm;
    bool canDoubleJump = true;

    public override void JumpCheck(ref float xVel, ref float yVel)
    {
        if (!isAirborne)
        {
            canDoubleJump = true;
        }

        if ((!isAirborne || (isAirborne && isWallborne != WallState.NOT_WALLBORNE)) && Input.GetKeyDown(jumpKey))
        {
            //Player is either grounded OR on a wall and jumps
            yVel += jumpVel;
            isAirborne = true;
        }
        else if (canDoubleJump && Input.GetKeyDown(jumpKey))
        {
            //Player isn't able to jump normally and must double jump, but can
            yVel += jumpVel;
            isAirborne = true;
            spriteObject.GetComponent<Animator>().SetTrigger("Double Jumped");
            canDoubleJump = false;
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

    public override void PlayJump() {
        spriteObject.GetComponent<Animator>().Play("Lity_Jump");
    }
}
