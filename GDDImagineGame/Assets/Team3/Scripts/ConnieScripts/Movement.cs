using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum PlayerType
    {
        Knight,
        Witch,
        Wizard,
        Dragon
    }

    //fields for player class.
    private Rigidbody rb;
    private bool isFalling;
    private int score;

    public int ogMultiplier;
    public int multiplier;
    public int maxJumpVelocity;
    public int controller;
    public PlayerType type; 

    public int coinMultiplier = 1;
    public bool isInvincible = false;

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
        ogMultiplier = multiplier;
    }


    public bool IsGrounded()
    {
        return Physics.CheckCapsule(gameObject.GetComponent<Collider>().bounds.center, new Vector3(gameObject.GetComponent<Collider>().bounds.center.x,
            gameObject.GetComponent<Collider>().bounds.min.y - 0.6f, gameObject.GetComponent<Collider>().bounds.center.z), 0.18f);
    }
    void Update()
    {
        if (!isFalling && IsGrounded())
        {
            rb.AddForce(new Vector3(0, Input.GetAxis("Player" + controller + "-A") * 50 * rb.mass,0));
        }

        if(rb.velocity.y >= maxJumpVelocity && !isFalling)
        {
            this.isFalling = true;
            multiplier /= 2;

        }
        else if(rb.velocity.y == 0)
        {
            isFalling = false;
            multiplier = ogMultiplier;
        }

        rb.velocity = new Vector3(Input.GetAxis("Player" + controller + "-LeftJoy-X") * multiplier,
            rb.velocity.y, Input.GetAxis("Player" + controller + "-LeftJoy-Y") * multiplier);

        //cease movement if no input
        if (Input.GetAxis("Player" + controller + "-LeftJoy-Y") == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        if (Input.GetAxis("Player" + controller + "-LeftJoy-X") == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }

        if (Input.GetAxis("Player" + controller + "-B") == 1)
        {
            switch (type)
            {
                case PlayerType.Dragon:
                    break;
                case PlayerType.Knight:
                    gameObject.GetComponent<Renderer>().enabled = false;
                    gameObject.GetComponent<Collider>().enabled = false;
                    break;
                case PlayerType.Witch:
                    break;
                case PlayerType.Wizard:
                    break;
            }
        }
    }
}
