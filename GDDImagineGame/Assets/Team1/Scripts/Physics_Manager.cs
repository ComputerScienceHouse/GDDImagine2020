using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Manager : MonoBehaviour
{
    private float gravity;
    // Start is called before the first frame update
    void Start()
    {
        gravity = 9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// applies gravity to active darts
    /// </summary>
    /// <param name="d">the dart to which you are applyng gravity</param>
    public void ApplyGravity(Dart d)
    {
        d.VelocityY -= gravity;
    }
}
