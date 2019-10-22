using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Go For Main Pot");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Steal");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Block");
        }
    }
}
