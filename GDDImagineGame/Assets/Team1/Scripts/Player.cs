using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerOptions // Options for different controllers 
{
    Player_1,
    Player_2,
    Player_3,
    Player_4
}

public class Player : MonoBehaviour
{
    

    public ControllerOptions playerNumber; 
    string controllerXName; // The name of the controllers x value
    string controllerYName; // The name of the controllers y value
    string buttonName;
    Vector3 mouseWorldPos;
    float angle;
    //GameObject arrow;
    public float power;
    public float maxPower;
    public float powerIncrement;
    bool powerUpDown;
    public GameObject dartPrefab;
    bool buttonDown;
    // Start is called before the first frame update
    void Start()
    {
        //arrow = GameObject.FindGameObjectWithTag("Arrow");
        SetController();
        power = 0f;
        maxPower = 1f;
        powerIncrement = .01f;
        powerUpDown = true;
        buttonDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
        AngleInput();
        getInput();
    }

    /// <summary>
    /// Sets the name of the players controller
    /// </summary>
    void SetController()
    {
        switch (playerNumber)
        {
            case ControllerOptions.Player_1:
                controllerXName = "LeftJoystick X";
                controllerYName = "LeftJoystick Y";
                buttonName = "A";
                break;
            case ControllerOptions.Player_2:
                controllerXName = "LeftJoystick X2";
                controllerYName = "LeftJoystick Y2";
                buttonName = "A 2";
                break;
            case ControllerOptions.Player_3:
                controllerXName = "LeftJoystick X3";
                controllerYName = "LeftJoystick Y3";
                buttonName = "A 3";
                break;
            case ControllerOptions.Player_4:
                controllerXName = "LeftJoystick X4";
                controllerYName = "LeftJoystick Y4";
                buttonName = "A 4";
                break;
        }        
    }


    /// <summary>
    /// Gets angle from controller left joystick
    /// </summary>
    void AngleInput()
    {
        angle = Mathf.Atan2(-Input.GetAxis(controllerYName), -Input.GetAxis(controllerXName));
        //Debug.Log(angle);
    }

    void getInput()
    {
        buttonDown = Input.GetButton(buttonName); 
  
        if (buttonDown)
        {
            if (powerUpDown) power += powerIncrement; 
            else power -= powerIncrement;
            if (power > maxPower || power < 0) powerUpDown = !powerUpDown;
            Debug.Log(power);
        }

        if (Input.GetButtonUp(buttonName))
        {
            spawnDart();
        }

        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Input.GetAxis("LeftJoystick Y"), 0);
        //Debug.Log(Input.GetAxis("LeftJoystick Y"));

    }

    void RotateToMouse()
    {
        mouseWorldPos = /*Input.mousePosition;*/Camera.main.ScreenToWorldPoint(Input.mousePosition + gameObject.transform.position) ;
        angle = (Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg);
        //print(angle);
        //arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void spawnDart()
    {
        dartPrefab = Instantiate(dartPrefab, gameObject.transform.position+new Vector3(2, -.5f, 0), Quaternion.identity);
        dartPrefab.AddComponent<Dart>();
        dartPrefab.GetComponent<Dart>().Throw(power, angle, this);
        power = 0;
    }
}
