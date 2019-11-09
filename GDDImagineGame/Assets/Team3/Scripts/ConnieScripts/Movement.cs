using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //fields for player class.
    private Rigidbody rb;
    private bool isFalling;
    private int score;

    public int multiplier;
    public int maxJumpVelocity;
    public int controller;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFalling = false;

    }


    public bool IsGrounded()
    {
        return Physics.CheckCapsule(gameObject.GetComponent<Collider>().bounds.center, new Vector3(gameObject.GetComponent<Collider>().bounds.center.x,
            gameObject.GetComponent<Collider>().bounds.min.y - 0.6f, gameObject.GetComponent<Collider>().bounds.center.z), 0.18f);
    }
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal" + controller.ToString()) * multiplier,
            rb.velocity.y, Input.GetAxis("Vertical" + controller.ToString()) * multiplier);


        if (!isFalling && IsGrounded())
        {
            rb.AddForce(new Vector3(0, Input.GetAxis("Jump" + controller.ToString()) * 50, 0));
        }

        if(rb.velocity.y >= maxJumpVelocity && !isFalling)
        {
            this.isFalling = true;
        }
        else if(rb.velocity.y == 0)
        {
            isFalling = false;
        }

        //cease movement if no input
        if (Input.GetAxis("Vertical" + controller.ToString()) == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        if (Input.GetAxis("Horizontal1" + controller.ToString()) == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }
}
