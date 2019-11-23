using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 mouseWorldPos;
    float angle;
    //GameObject arrow;
    public float power;
    public float maxPower;
    public float powerIncrement;
    bool powerUpDown;
    public GameObject dartPrefab;
    bool currentInput;
    bool previousInput;
    // Start is called before the first frame update
    void Start()
    {
        //arrow = GameObject.FindGameObjectWithTag("Arrow");
        power = 0f;
        maxPower = 1f;
        powerIncrement = .01f;
        powerUpDown = true;
        previousInput = false;
        currentInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
        getInput();
    }

    void getInput()
    {
        currentInput = Input.GetMouseButton(0);
        if (currentInput)
        {
            if (powerUpDown) power += powerIncrement; 
            else power -= powerIncrement;
            if (power > maxPower || power < 0) powerUpDown = !powerUpDown;
            Debug.Log(power);
        }

        if (previousInput && !currentInput)
        {
            spawnDart();
        }

        previousInput = currentInput;
        
        
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Input.GetAxis("LeftJoystick Y"), 0);
        //Debug.Log(Input.GetAxis("LeftJoystick Y"));
       
    }

    void RotateToMouse()
    {
        mouseWorldPos = /*Input.mousePosition;*/Camera.main.ScreenToWorldPoint(Input.mousePosition + gameObject.transform.position) ;
        angle = (Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg);
        print(angle);
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
