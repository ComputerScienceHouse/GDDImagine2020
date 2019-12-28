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
	public bool animating;

    void Start()
    {
        // Initialize player vars
        score = 0;
        choice = Choice.None;
    }

    void Update()
    {
		// If this player is animating, animate
		if (animating)
		{
			animating = false;
		}

		// Otherwise, read input
		else
		{
			PlayerAction();
		}
        
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

	//helper function for a successful pot steal animation
	public void PlayerMoveToPotSuccessFul()
	{
		animating = true;
	}

	//helper function for a unsuccessful pot steal animation
	public void PlayerMoveToPotUnsuccessFul()
	{
		animating = true;
	}

	//helper function for a unsuccessful pot steal animation
	public void PlayerMoveToStealSuccessFul(float degreesToRotate)
	{
		animating = true;
	}

	//helper function for a unsuccessful pot steal animation
	public void PlayerMoveToStealUnsuccessFul(float degreesToRotate)
	{
		animating = true;
	}

	//helper function for a block animation
	public void PlayerMoveToBlock()
	{
		animating = true;
	}
}
