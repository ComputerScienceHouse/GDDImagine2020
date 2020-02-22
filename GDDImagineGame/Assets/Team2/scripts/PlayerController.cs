using UnityEngine;

// Enumeration of the actions a player can take in a round
public enum Choice
{
    Pot,
    StealLeft,
    StealAcross,
    StealRight,
    Block,
    None
}

// Enumeration of the animations a player can perform
public enum Anim
{
    SuccessfulPot,
    UnsuccessfulPot,
    SuccessfulSteal,
    UnsuccessfulSteal,
    Block,
    None
}

public class PlayerController : MonoBehaviour
{
    // Player's current score
    public int score;

    // Player's action for any given round
    public Choice choice;

    // The keys this player must press to perform various actions
    public KeyCode potButton;
    public KeyCode stealButton;
    public KeyCode blockButton;
    public KeyCode stealLeft;
    public KeyCode stealAcross;
    public KeyCode stealRight;

    // Animation
    public Anim anim;
    public float animTime;
    public float startTime;
    public Vector3 targetPos;
    private float timeElapsed;
    private float journeyFraction;
    private Vector3 originPos;
    private Quaternion originRot;
    private Vector3 potPos;
    private Vector3 originRelCenter;
    private Vector3 targetRelCenter;

    void Start()
    {
        // Initialize player vars
        score = 0;
        choice = Choice.None;
        anim = Anim.None;
        startTime = 0.0f;
        timeElapsed = 0.0f;
        journeyFraction = 0.0f;
        originPos = transform.position;
        originRot = transform.rotation;
        targetPos = Vector3.zero;
        potPos = GameObject.Find("Manager").GetComponent<Manager>().pot.transform.position;
        originRelCenter = originPos - potPos;
        targetRelCenter = Vector3.zero;
    }

    void Update()
    {
        // handle player input
        PlayerAction();
    }

    ///
    /// Check for user input and print a message on the console that corresponds with the key that the user pressed
    ///
    public void PlayerAction()
    {
        // Only let player make choice if none has been made yet
        if (choice == Choice.None)
        {
            // Player wants to go for pot
            if (Input.GetKeyDown(potButton))
            {
                Debug.Log(name + " is going for main pot");
                choice = Choice.Pot;
            }
            // Player wants to go for a steal, handle who they're trying to steal from
            else if (Input.GetKey(stealButton))
            {
                if (Input.GetKeyDown(stealLeft))
                {
                    Debug.Log(name + " is stealing from the player to the left of them");
                    choice = Choice.StealLeft;
                }
                else if (Input.GetKeyDown(stealAcross))
                {
                    Debug.Log(name + " is stealing from the player across from them");
                    choice = Choice.StealAcross;
                }
                else if (Input.GetKeyDown(stealRight))
                {
                    Debug.Log(name + " is stealing from the player to the right of them");
                    choice = Choice.StealRight;
                }
            }
            // Player wants to block
            else if (Input.GetKeyDown(blockButton))
            {
                Debug.Log(name + " is blocking");
                choice = Choice.Block;
            }
        }
    }

    // Helper function for handling general animation of this player
    public void PlayerAnimation()
    {
        // Set up relative target center based on the targetPos that should've been updated by the manager
        if (targetRelCenter == Vector3.zero)
            targetRelCenter = targetPos - potPos;

        timeElapsed = Time.time - startTime;
        if (timeElapsed <= animTime / 2)
        {
            journeyFraction = timeElapsed / animTime * 2;
            Debug.Log("Leaving origin");
        }
        else
        {
            journeyFraction = 2.0f - timeElapsed / animTime * 2;
            Debug.Log("Returning to origin");
        }
        // Jump to helper function that's relevant to this player currently
        switch (anim)
        {
            case Anim.SuccessfulPot:
                Debug.Log("Animating a successful pot grab");
                PlayerMoveToPotSuccessFul();
                break;
            case Anim.UnsuccessfulPot:
                Debug.Log("Animating an unsuccessful pot grab");
                PlayerMoveToPotUnsuccessFul();
                break;
            case Anim.SuccessfulSteal:
                Debug.Log("Animating a successful steal");
                PlayerMoveToStealSuccessFul();
                break;
            case Anim.UnsuccessfulSteal:
                Debug.Log("Animating an unsuccessful steal");
                PlayerMoveToStealUnsuccessFul();
                break;
            case Anim.Block:
                Debug.Log("Animating a block");
                PlayerMoveToBlock();
                break;
            default:
                break;
        }
        // Reset this instance when the time to complete the animation has elapsed
        if (timeElapsed >= animTime)
        {
            transform.position = originPos;
            transform.rotation = originRot;
            targetRelCenter = Vector3.zero;
            anim = Anim.None;
        }
    }

    // Helper function for a successful pot steal animation
    public void PlayerMoveToPotSuccessFul()
    {
        transform.position = Vector3.Lerp(originPos, potPos, journeyFraction);
        Vector3 temp = transform.position;
        temp.y = 1.0f;
        transform.position = temp;
    }

    // Helper function for a unsuccessful pot steal animation
    public void PlayerMoveToPotUnsuccessFul()
    {
        transform.position = Vector3.Lerp(originPos, potPos, journeyFraction);
        Vector3 temp = transform.position;
        temp.y = 1.0f;
        transform.position = temp;
    }

    // Helper function for a unsuccessful pot steal animation
    public void PlayerMoveToStealSuccessFul()
    {
        // Interpolate over the arc relative to center (pot)
        transform.position = Vector3.Slerp(originRelCenter, targetRelCenter, journeyFraction);
        Vector3 temp = transform.position;
        temp.y = 1.0f;
        transform.position = temp;
    }

    //helper function for a unsuccessful pot steal animation
    public void PlayerMoveToStealUnsuccessFul()
    {
        // Interpolate over the arc relative to center (pot)
        transform.position = Vector3.Slerp(originRelCenter, targetRelCenter, journeyFraction);
        Vector3 temp = transform.position;
        temp.y = 1.0f;
        transform.position = temp;
    }

    //helper function for a block animation
    public void PlayerMoveToBlock()
    {
        transform.rotation = Quaternion.Euler(0, journeyFraction * 360, 0);
        Vector3 temp = transform.position;
        temp.y = 1.0f;
        transform.position = temp;
    }
}
