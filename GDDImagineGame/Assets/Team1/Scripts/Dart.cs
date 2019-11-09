using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity);
    }

    /// <summary>
    /// called upon creation of the dart to initialize its velocity
    /// </summary>
    /// <param name="xVal">the x value returned by the controller</param>
    /// <param name="yVal">the y value returned by the controller</param>
    public void Throw(float xVal, float yVal)
    {
        velocity = new Vector3(xVal, yVal, 0);
    }
}
