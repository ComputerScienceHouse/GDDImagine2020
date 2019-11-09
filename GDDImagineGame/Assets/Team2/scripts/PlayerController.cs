using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Choice
{
    pot,
    steal,
    block,
    none
}

public class PlayerController : MonoBehaviour
{

    public int score;
    public Choice choice;
    public KeyCode potButton;
    public KeyCode stealButton;
    public KeyCode blockButton;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        choice = Choice.none;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
    }

    /// <summary>
    /// Check for user input and print a message on the console that corresponds with the key that the user pressed
    /// </summary>
    public void PlayerAction()
    {
        if (choice == Choice.none)
        {
            if (Input.GetKeyDown(potButton))
            {
                Debug.Log(name + " is going for main pot");
                choice = Choice.pot;
            }
            else if (Input.GetKeyDown(stealButton))
            {
                Debug.Log(name + " is stealing");
                choice = Choice.steal;
            }
            else if (Input.GetKeyDown(blockButton))
            {
                Debug.Log(name + " is going for blocking");
                choice = Choice.block;
            }
        }
    }
}
