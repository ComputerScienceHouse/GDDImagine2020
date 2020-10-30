﻿using UnityEngine;
using UnityEngine.UI;

// Enumeration of the actions a player can take in a round
public enum Choice
{
    Pot,
    StealLeft,
    StealRight,
    Block,
    None
}

public class PlayerController : MonoBehaviour
{
    // Player's current score
    [SerializeField] private int score;

    // Player's action for any given round
    [SerializeField] private Choice choice;

    // The keys this player must press to perform various actions
    public KeyCode potButton;
    public KeyCode blockButton;
    public KeyCode stealLeft;
    public KeyCode stealRight;

    public Text readyText;

    public Manager manager;


    //Player Properties
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
    public Choice Choice
    {
        get
        {
            return choice;
        }
        set
        {
            choice = value;
        }
    }


    void Start()
    {
        // Initialize player vars
        Score = 10;
        Choice = Choice.None;
    }

    void Update()
    {
        if (manager.Timer != 0)
        {
            PlayerAction();
        }

        if (manager.Timer == 0)
        {
            readyText.enabled = false;
        }
    }

    ///
    /// Check for user input and print a message on the console that corresponds with the key that the user pressed
    ///
    public void PlayerAction()
    {
        // Only let player make choice if none has been made yet
        if (Choice == Choice.None)
        {
            // Player wants to go for pot
            if (Input.GetKeyDown(potButton))
            {
                Debug.Log(name + " is going for main pot");
                Choice = Choice.Pot;
                readyText.enabled = true;
            }
            // Player wants to go for a steal, handle who they're trying to steal from
            else if (Input.GetKeyDown(stealLeft))
            {
                Debug.Log(name + " is stealing from the player to the left of them");
                Choice = Choice.StealLeft;
                readyText.enabled = true;
            }
            else if (Input.GetKeyDown(stealRight))
            {
                Debug.Log(name + " is stealing from the player to the right of them");
                Choice = Choice.StealRight;
                readyText.enabled = true;
            }

            // Player wants to block
            else if (Input.GetKeyDown(blockButton))
            {
                Debug.Log(name + " is blocking");
                Choice = Choice.Block;
                readyText.enabled = true;
            }
        }
    }
}
