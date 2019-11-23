using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Vector3 velocity;
    public GameObject player;
    private Player thrower;

    public Player Thrower
    {
        get
        {
            return thrower;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("player");
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(velocity);
       gameObject.transform.Translate(velocity);
    }

    /// <summary>
    /// Throws the dart upon the player's command
    /// </summary>
    public void Throw(float power, float angle, Player thrower)
    {
        this.thrower = thrower;
        float X = -power * Mathf.Cos(angle);
        float Y = power * Mathf.Sin(angle);
        velocity = new Vector3(X,Y, 0);
    }

    
}
