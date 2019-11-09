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

    
    public void ApplyGravity(Dart d)
    {
        d.VelocityY -= gravity;
    }
}
