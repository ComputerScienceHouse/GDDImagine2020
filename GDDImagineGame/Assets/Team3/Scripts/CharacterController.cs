using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    float multiplier = 10, maxJumpVelocity = 5;
    public int score = 0;
    bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * multiplier, rb.velocity.y, Input.GetAxis("Vertical") * multiplier);

        if (!isFalling)
        {
            rb.AddForce(new Vector3(0, Input.GetAxis("Jump") * 50, 0));
        }
        if(rb.velocity.y >= maxJumpVelocity && !isFalling)
        {
            isFalling = true;
        }
        else if(rb.velocity.y == 0)
        {
            isFalling = false;
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        transform.Rotate(0, Input.GetAxis("CameraX"), 0);
    }
}
