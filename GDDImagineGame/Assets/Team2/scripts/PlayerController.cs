using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum for choices a player can make
public enum Choice
{
    pot,
    stealLeft,
    stealAcross,
    stealRight,
    block,
    none
}

public class PlayerController : MonoBehaviour
{

    //vars for players
    public int score;
    //public bool animating;
    //public bool firstHalf;
    public Choice choice;
    public KeyCode potButton;
    public KeyCode stealButton;
    public KeyCode blockButton;
    public KeyCode stealLeft;
    public KeyCode stealAcross;
    public KeyCode stealRight;

    // Start is called before the first frame update
    void Start()
    {
        //initialize some vars
        score = 0;
        //animating = false;
        //firstHalf = true;
        choice = Choice.none;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        //if (animating)
        //{
        //    if (firstHalf)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}
    }

    /// <summary>
    /// Check for user input and print a message on the console that corresponds with the key that the user pressed
    /// </summary>
    public void PlayerAction()
    {
        //only let player make choice if none have been made yet
        if (choice == Choice.none)
        {
            //player wants to go for pot
            if (Input.GetKeyDown(potButton))
            {
                Debug.Log(name + " is going for main pot");
                choice = Choice.pot;
            }
            //player wants to go for a steal, handle who they're trying to steal from
            else if (Input.GetKey(stealButton))
            {
                if (Input.GetKeyDown(stealLeft))
                {
                    Debug.Log(name + " is stealing from the player to the left of them");
                    choice = Choice.stealLeft;
                }
                else if (Input.GetKeyDown(stealAcross))
                {
                    Debug.Log(name + " is stealing from the player across from them");
                    choice = Choice.stealAcross;
                }
                else if (Input.GetKeyDown(stealRight))
                {
                    Debug.Log(name + " is stealing from the player to the right of them");
                    choice = Choice.stealRight;
                }
            }
            //player wants to block
            else if (Input.GetKeyDown(blockButton))
            {
                Debug.Log(name + " is going for blocking");
                choice = Choice.block;
            }
        }
    }

    ////helper function for a successful pot steal animation
    //public void PlayerMoveToPotSuccessFul()
    //{
    //    animating = true;
    //}

    ////helper function for a unsuccessful pot steal animation
    //public void PlayerMoveToPotUnsuccessFul()
    //{
    //    animating = true;
    //}

    ////helper function for a unsuccessful pot steal animation
    //public void PlayerMoveToStealSuccessFul(float degreesToRotate)
    //{
    //    animating = true;

    //}

    ////helper function for a unsuccessful pot steal animation
    //public void PlayerMoveToStealUnsuccessFul(float degreesToRotate)
    //{
    //    animating = true;
    //}

    ////helper function for a block animation
    //public void PlayerMoveToBlock()
    //{
    //    animating = true;

    //}
}
