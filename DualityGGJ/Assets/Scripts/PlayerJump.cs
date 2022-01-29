using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    public float fallMultiplier;
    [SerializeField]
    public float jumpMultiplier;
    
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody.velocity.y < 0) {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rigidbody.velocity.y > 0 && Input.GetButtonDown("Jump")) {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }       
    }
}
