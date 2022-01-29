using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lity : Player
{
    [SerializeField]
    GameObject ballForm;

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
