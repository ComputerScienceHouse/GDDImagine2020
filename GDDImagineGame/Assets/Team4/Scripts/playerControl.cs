using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 currentMove;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        currentMove = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        playerController();
    }

    void playerController()
    {
        int layerMask = 0; // This can be zero, it then stops the weird collision thing
        RaycastHit hit;

        float move = speed * Time.deltaTime;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //transform.position = Vector3.MoveTowards(transform.position, tf.position, move); // Smooth camera movements

        currentMove = Vector3.zero;  // If player is not moving and has not hit a wall, the default movement vector is 0

        if (moveHorizontal == 1)
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1, layerMask))  // Checks if object global right raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.right * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else 
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.right, Color.white);
                Debug.Log("Did Not Hit");

                currentMove = Vector3.right * move;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.right);  // Player facing direction is updated to right
        }

        else if (moveHorizontal == -1)
        {
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 1, layerMask))  // Checks if object global left raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.left * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.left, Color.white);
                Debug.Log("Did Not Hit");

                currentMove = Vector3.left * move;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.left);  // Player facing direction is updated to left
        }

        else if (moveVertical == 1)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1, layerMask))  // Checks if object global forwards raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.forward * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.forward, Color.white);
                Debug.Log("Did Not Hit");

                currentMove = Vector3.forward * move;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward);  // Player facing direction is updated to forwards
        }

        else if (moveVertical == -1)
        {
            if (Physics.Raycast(transform.position, Vector3.back, out hit, 1, layerMask))  // Checks if object global backwards raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.back * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.back, Color.white);
                Debug.Log("Did Not Hit");

                currentMove = Vector3.back * move;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.back);  // Player facing direction is updated to backwards
        }

        transform.Translate(currentMove, Space.World);  // Player position is updated
    }
}
