using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private float velocityX;
    private float velocityY;

    public float VelocityX
    {
        get
        {
            return velocityX;
        }
    }

    public float VelocityY
    {
        get
        {
            return velocityY;
        }
        set
        {
            velocityY = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(float power, float angle)
    {

    }
}
