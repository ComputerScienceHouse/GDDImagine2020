using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModPlayer(Movement player);

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

    public float speedMultiplier;
    public int maxJumpVelocity;
    public int controller;
    public PlayerType type;

    public float speedBoostMultiplier = 1;
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
            rb.velocity = new Vector3(Input.GetAxis("Player" + controller + "-LeftJoy-X") * (speedMultiplier / 2) * speedBoostMultiplier,
                rb.velocity.y, Input.GetAxis("Player" + controller + "-LeftJoy-Y") * (speedMultiplier / 2) * speedBoostMultiplier);

        }
        else if(rb.velocity.y == 0)
        {
            isFalling = false;
            rb.velocity = new Vector3(Input.GetAxis("Player" + controller + "-LeftJoy-X") * speedMultiplier * speedBoostMultiplier,
                rb.velocity.y, Input.GetAxis("Player" + controller + "-LeftJoy-Y") * speedMultiplier * speedBoostMultiplier);
        }

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

    /// <summary>
    /// Applies a certain buff or debuff permanently
    /// </summary>
    /// <param name="playerMod">Buff or debuff that is applied to the player</param>
    public void ApplyPlayerMod(ModPlayer playerMod)
    {
        playerMod(this);
    }

    /// <summary>
    /// Applies a certain buff or debuff to a player for a certain time and then sets it back
    /// </summary>
    /// <param name="playerMod">Buff or debuff that is applied to the player</param>
    /// <param name="time">The time the buff or debuff lasts for in seconds</param>
    /// <param name="callback">The callback function that resets the modified values</param>
    public void ApplyPlayerMod(ModPlayer playerMod, float time, ModPlayer callback)
    {
        playerMod(this);
        StartCoroutine(PlayerModCallback(time, callback));
    }

    /// <summary>
    /// Coroutine that calls the player mod callback function after a specified time
    /// </summary>
    /// <param name="time">The time the buff or debuff lasts for in seconds</param>
    /// <param name="callback">The callback function that resets the modified values</param>
    /// <returns></returns>
    private IEnumerator PlayerModCallback(float time, ModPlayer callback)
    {
        yield return new WaitForSecondsRealtime(time);
        callback(this);
    }
}
