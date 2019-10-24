using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControls : MonoBehaviour
{
    public Transform tf;
    private CharacterController charCtrl;
    private Quaternion rotation;
    private float speed;
    private float rotationSpeed;
    private Vector3 position;
    private Vector3 currentFace;
    private Vector3 currentMove;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        speed = 10.0f;
        charCtrl = GetComponent<CharacterController>();

        rotationSpeed = 100.0f;
        position = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        // testController3();
        // testController2();
        // testController();
        characterControl();
    }

    void characterControl()
    {
        float move = speed * Time.deltaTime;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.rotation = Quaternion.LookRotation(movement);

        transform.Translate(movement * move, Space.World);
    }

    void testController()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        float move = speed * Time.deltaTime;
        float rotate = rotationSpeed * Time.deltaTime;

        Vector3 moveUp = new Vector3(0, 0, speed * Time.deltaTime);
        Vector3 moveDown = new Vector3(0, 0, -speed * Time.deltaTime);
        Vector3 moveRight = new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 moveLeft = new Vector3(-speed * Time.deltaTime, 0, 0);

        Vector3 faceUp = new Vector3(0, 0, 1);
        Vector3 faceDown = new Vector3(0, 0, -1);
        Vector3 faceRight = new Vector3(1, 0, 0);
        Vector3 faceLeft = new Vector3(-1, 0, 0);

        transform.position = Vector3.MoveTowards(transform.position, tf.position, move);

        if (currentFace.magnitude != 0) // Causes player to face most recent direction when stationary
        {
            transform.rotation = Quaternion.LookRotation(currentFace);
        }

        if (Input.GetKey(KeyCode.D))
        {
            currentFace = faceRight;
            if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, 1, layerMask)) // D
            {
                Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

                moveRight = Vector3.zero;
            }
            else if (!Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, 1, layerMask))
            {
                Debug.DrawRay(transform.position, new Vector3(1, 0, 0), Color.white);
                Debug.Log("Did Not Hit");
            }
            currentMove = moveRight;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            // transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.A))
        {
            currentFace = faceLeft;
            if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 1, layerMask)) // A
            {
                Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

                moveLeft = Vector3.zero;
            }
            else if (!Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 1, layerMask))
            {
                Debug.DrawRay(transform.position, new Vector3(-1, 0, 0), Color.white);
                Debug.Log("Did Not Hit");
            }
            currentMove = moveLeft;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            // transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.W))
        {
            currentFace = faceUp;
            if (Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit, 1, layerMask)) // W
            {
                Debug.DrawRay(transform.position, new Vector3(0, 0, 1) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

                moveUp = Vector3.zero;
            }
            else if (!Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit, 1, layerMask))
            {
                Debug.DrawRay(transform.position, new Vector3(0, 0, 1), Color.white);
                Debug.Log("Did Not Hit");
            }
            currentMove = moveUp;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            // transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.S))
        {
            currentFace = faceDown;
            if (Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 1, layerMask)) // S
            {
                Debug.DrawRay(transform.position, new Vector3(0, 0, -1) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");

                moveDown = Vector3.zero;
            }
            else if (!Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 1, layerMask))
            {
                Debug.DrawRay(transform.position, new Vector3(0, 0, -1), Color.white);
                Debug.Log("Did Not Hit");
            }
            currentMove = moveDown;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            // transform.rotation = Quaternion.LookRotation(currentMove); // Turning is more immediate
        }
    }

    void testController2()
    {
        float move = speed * Time.deltaTime;
        float rotate = rotationSpeed * Time.deltaTime;

        Vector3 moveUp = new Vector3(0, 0, speed * Time.deltaTime);
        Vector3 moveDown = new Vector3(0, 0, -speed * Time.deltaTime);
        Vector3 moveRight = new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 moveLeft = new Vector3(-speed * Time.deltaTime, 0, 0);

        transform.position = Vector3.MoveTowards(transform.position, tf.position, move);
        transform.rotation = Quaternion.LookRotation(currentMove);

        if (Input.GetKey(KeyCode.D))
        {
            currentMove = moveRight;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            currentMove = moveLeft;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            currentMove = moveUp;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            currentMove = moveDown;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
        }
    }

    void testController3()
    {
        // int layerMask = 1 << 8; // I don't understand what this does
        // layerMask = ~layerMask; // I don't understand what this does
        // RaycastHit hit;

        float move = speed * Time.deltaTime;
        float rotate = rotationSpeed * Time.deltaTime;

        Vector3 moveUp = new Vector3(0, 0, speed * Time.deltaTime);
        Vector3 moveDown = new Vector3(0, 0, -speed * Time.deltaTime);
        Vector3 moveRight = new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 moveLeft = new Vector3(-speed * Time.deltaTime, 0, 0);

        transform.position = Vector3.MoveTowards(transform.position, tf.position, move);

        if (currentMove.magnitude != 0) // Causes player to face most recent direction when stationary
        {
            transform.rotation = Quaternion.LookRotation(currentMove);
        }

        if (Input.GetKey(KeyCode.D))
        {
            currentMove = moveRight;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.A))
        {
            currentMove = moveLeft;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.W))
        {
            currentMove = moveUp;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.LookRotation(currentMove);  // Turning is more immediate
        }

        else if (Input.GetKey(KeyCode.S))
        {
            currentMove = moveDown;
            tf.Translate(currentMove, Space.World);
            // currentFace = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.LookRotation(currentMove); // Turning is more immediate
        }
    }

    bool rightCollision()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)) * 1, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }

    bool leftCollision()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 1, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }

    bool backCollision()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)) * 1, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }

    bool forwardCollision()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), out hit, 1, layerMask))
        {


            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {


            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)) * 1, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }
}
