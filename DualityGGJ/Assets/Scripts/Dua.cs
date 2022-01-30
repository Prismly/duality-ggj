using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dua : Player
{
    [SerializeField]
    GameObject ballForm, shadowForm;

    KeyCode dashRollKey = KeyCode.DownArrow;
    DashRollStates dashRollState = DashRollStates.READY;
    float timerDR = 0f;
    float spinDurationDR = 0.25f;
    float cooldownDurationDR = 1f;
    float sprintSpeed = 20f;
    float originalSpeedCap = 15f;
    [SerializeField]
    float timerShadow, timerShadowMax;

    enum DashRollStates
    {
        READY,
        SPINNING,
        DIVING,
        SPRINTING,
        COOLDOWN
    }

    public override void InputChecks()
    {
        switch (dashRollState)
        {
            case DashRollStates.READY:
                {
                    if (Input.GetKeyDown(dashRollKey) && isWallborne == WallState.NOT_WALLBORNE)
                    {
                        Debug.Log("Switch to SPINNING");
                        spriteObject.GetComponent<Animator>().SetTrigger("Pound Dash Windup");
                        dashRollState = DashRollStates.SPINNING;
                        rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
                        timerDR = 0;
                    }
                    break;
                }
            case DashRollStates.SPINNING:
                {
                    if (timerDR < spinDurationDR)
                    {
                        timerDR += Time.deltaTime;
                    }
                    else
                    {
                        Debug.Log("Switch to DIVING");
                        dashRollState = DashRollStates.DIVING;
                        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                        speedCapX = 1;
                        speedCapY = 20;
                        rigidbody.velocity = Vector2.down * 20;
                    }
                    break;
                }
            case DashRollStates.SPRINTING:
                {
                    timerShadow += Time.deltaTime;
                    Debug.Log(timerShadow);
                    if (timerShadow > timerShadowMax)
                    {
                        timerShadow = 0;
                        Debug.Log("spawn shadow");
                        GameObject newShadow = Instantiate(shadowForm);
                        newShadow.GetComponent<SpriteRenderer>().sprite = spriteObject.GetComponent<SpriteRenderer>().sprite;
                        newShadow.transform.position = transform.position;
                    }

                    if ((facingRight && rigidbody.velocity.x < originalSpeedCap) ||
                        (!facingRight && rigidbody.velocity.x > -originalSpeedCap))
                    {
                        speedCapX = originalSpeedCap;
                        Debug.Log("Switch to COOLDOWN");
                        dashRollState = DashRollStates.COOLDOWN;
                        timerDR = 0;
                    }
                    break;
                }
            case DashRollStates.COOLDOWN:
                {
                    if (timerDR < cooldownDurationDR)
                    {
                        timerDR += Time.deltaTime;
                    }
                    else
                    {
                        Debug.Log("Switch to READY");
                        spriteObject.GetComponent<Animator>().SetTrigger("Pound Dash Cooldown Expired");
                        dashRollState = DashRollStates.READY;
                    }
                    break;
                }
        }

        base.InputChecks();
    }

    public override void TransformCheck()
    {
        if (Input.GetKeyDown(transformKey))
        {
            GameObject newForm = Instantiate(ballForm);
            newForm.transform.position = transform.position;
            cameraController.player = newForm.transform;
            newForm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            newForm.GetComponent<Ball>().isDua = false;
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (dashRollState == DashRollStates.DIVING)
        {
            Debug.Log("Switch to SPRINTING");
            spriteObject.GetComponent<Animator>().SetTrigger("Pound Dash Sprint Starts");
            dashRollState = DashRollStates.SPRINTING;
            speedCapX = sprintSpeed;
            speedCapY = originalSpeedCap;
            timerShadow = 0;
            if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                rigidbody.velocity = Vector2.left * sprintSpeed;
            }
            else if (!Input.GetKey(leftKey) && Input.GetKey(rightKey))
            {
                rigidbody.velocity = Vector2.right * sprintSpeed;
            }
            else
            {
                rigidbody.velocity = facingRight ? Vector2.right * sprintSpeed : Vector2.left * sprintSpeed;
            }
            timerDR = 0;
        }
	}
}
