using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Player
{
    [SerializeField]
    GameObject duaForm, lityForm;
    [SerializeField]
    GameObject explodeFX;
    public bool isDua;

    [SerializeField]
    public float explosiveForce;

    public override void TransformCheck()
    {
        if (Input.GetKeyUp(transformKey))
        {
            GameObject newForm = isDua ? Instantiate(duaForm) : Instantiate(lityForm);
            newForm.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Vector2 explodedVelocity = GetComponent<Rigidbody2D>().velocity;
            
            GameObject fx = Instantiate(explodeFX);
            fx.transform.position = newForm.transform.position;

            int[] checksX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] checksY = { 1, 1, 1, 0, 0, -1, -1, -1 };

            for(int i = 0; i < 8; i++)
            {
                Vector2 direction = new Vector2(checksX[i], checksY[i]);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, LayerMask.GetMask("Ground"));
                if (hit.collider != null)
                {
                    newForm.GetComponent<Rigidbody2D>().velocity += direction * explosiveForce;
                }
            }

            newForm.transform.position = transform.position;
            newForm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            Destroy(gameObject);
        }
    }
}
