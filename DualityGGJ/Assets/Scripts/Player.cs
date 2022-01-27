using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    public float pSpeed;
    [SerializeField]
    public float jumpVel;
    [SerializeField]
    public float decelarationConst;
    public bool isAirborne = false;

    KeyCode left = KeyCode.LeftArrow;
    KeyCode right = KeyCode.RightArrow;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(left))
            rigidbody.velocity -= new Vector2(pSpeed, 0) * Time.deltaTime;
        if (Input.GetKey(right))
            rigidbody.velocity += new Vector2(pSpeed, 0) * Time.deltaTime;
  
        if(rigidbody.velocity.x > 0 && !Input.GetKey(right))
            rigidbody.velocity -= new Vector2(decelarationConst, 0) * Time.deltaTime;
        if (rigidbody.velocity.x < 0 && !Input.GetKey(left))
            rigidbody.velocity += new Vector2(decelarationConst, 0) * Time.deltaTime;

        if(!isAirborne && Input.GetButtonDown("Jump")) {
            rigidbody.velocity += Vector2.up * jumpVel;
            isAirborne = true;
        }
    }
}
