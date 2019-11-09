using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 mouseWorldPos;
    float angle;
    GameObject arrow;
    public float power;
    public float maxPower;
    public float powerIncrement;
    bool powerUpDown;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        power = 0f;
        maxPower = 100f;
        powerIncrement = .25f;
        powerUpDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
        getInput();
    }

    void getInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (powerUpDown) power += powerIncrement;
            else power -= powerIncrement;
            if (power > maxPower || power < 0) powerUpDown = !powerUpDown;
            Debug.Log(power);
        }

        
        
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Input.GetAxis("LeftJoystick Y"), 0);
        //Debug.Log(Input.GetAxis("LeftJoystick Y"));
       
    }

    void RotateToMouse()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = (Mathf.Atan2(-mouseWorldPos.x, mouseWorldPos.y) * Mathf.Rad2Deg);
        //print(angle);
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
