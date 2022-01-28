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

    public override void TransformCheck()
    {
        if (Input.GetKeyUp(transformKey))
        {
            GameObject newForm = isDua ? Instantiate(duaForm) : Instantiate(lityForm);
            Vector2 explodedVelocity = GetComponent<Rigidbody2D>().velocity;

            
            GameObject fx = Instantiate(explodeFX);
            fx.transform.position = newForm.transform.position;
            
            int[8] checks1 = []

            for(int i = 0; i < 360; i += 45)
            {
                Debug.Log(Mathf.Cos(i));
                Vector2 direction = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, LayerMask.GetMask("Ground"));
                //Debug.Log(direction);
                if (hit.collider == null)
                {
                    //Debug.Log("no hit");
                }
                else
                {
                    //Debug.Log("hit");
                }
            }

            newForm.transform.position = transform.position;
            newForm.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            Destroy(gameObject);
        }
    }
}
