using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Vector3 velocity;
    private GameObject body;
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
        body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        body.
    }

    /// <summary>
    /// Throws the dart upon the player's command
    /// </summary>
    /// <param name="xVal">the X Value of the dart's velocity</param>
    /// <param name="yVal">the Y Value of the dart's velocity</param>
    /// <param name="player">the player that threw the dart</param>
    public void Throw(float xVal, float yVal, Player player)
    {
        velocity = new Vector3(xVal, yVal, 0);
        thrower = player;
    }
}
