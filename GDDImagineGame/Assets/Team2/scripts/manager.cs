using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    //private var for keeping track of the time
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check to see if time is up for picking an action
        if (timer >= 5.0f)
        {
            timer = 0.0f;
            //Resolve player choices here
            //After resolving choices and updating scores, check to see if the game would end here
        }
        else //default case, increment
        {
            timer += Time.deltaTime;
            /*
             Code for listening to player input and preventing them from making another choice would go here
             Use public game objects that can be set to specific object instances in the unity editor
             to set the variables that would be checked here
             */
        }
    }
}
