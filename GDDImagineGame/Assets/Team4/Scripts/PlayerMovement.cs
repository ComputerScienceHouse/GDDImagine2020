/*using UnityEngine;

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
}*/
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    public float speed;
    public float defaultSpeed;
    public float timeToFreeze;
    public static int playerScore;
    public static int enemyScore;

    private Vector3 currentMove;
    private Vector3 originalPosition;

    public int joystickNumber;

    void Start()
    {
        playerScore = 0;
        enemyScore = 0;
        speed = 10.0f;
        defaultSpeed = 10.0f;
        currentMove = Vector3.zero;
        this.originalPosition = this.gameObject.transform.position;
        timeToFreeze = 0.0f;
    }
 
    void FixedUpdate()
    {
        if (timeToFreeze > 0)
        {
            timeToFreeze -= Time.deltaTime;
        } 
        else
        {
            playerControl();
        }
    }

    //CANNOT TURN OFF COLLISIONS, BUT KEEP TRIGGERS ON!! WHY???
    /**
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("Holy shit dude, you killed him!");
        }
        else if (other.tag == "Enemy" && tag != "Enemy")
        {
            Debug.Log(transform.position);
            //transform.position = originalPosition;
        }
    
    }
    */
    void playerControl() 
    {
        string num = joystickNumber.ToString();

        float moveHorizontal = Input.GetAxisRaw($"LeftJoystickX_P{num}");
        float moveVertical = Input.GetAxisRaw($"LeftJoystickY_P{num}") * -1;

        int layerMask = 0; // This can be zero, it then stops the weird collision thing
        RaycastHit hit;

        float move = speed * Time.deltaTime;

        float reach = 0.51f;

        //transform.position = Vector3.MoveTowards(transform.position, tf.position, move); // Smooth camera movements

        currentMove = Vector3.zero;  // If player is not moving and has not hit a wall, the default movement vector is 0

        if (moveHorizontal >= 0.7f)
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, reach) && hit.transform.tag == "wall")  // Checks if object global right raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.right * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                currentMove = Vector3.zero; //could change, right now is here for testing purposes with collisions
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.right, Color.white);
                //Debug.Log("Did Not Hit");

                currentMove = Vector3.right * move * moveHorizontal;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.right);  // Player facing direction is updated to right
        }

        else if (moveHorizontal <= -0.7f)
        {
            if (Physics.Raycast(transform.position, Vector3.left, out hit, reach) && hit.transform.tag == "wall")  // Checks if object global left raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.left * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.left, Color.white);
                //Debug.Log("Did Not Hit");

                currentMove = Vector3.left * move * -moveHorizontal;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.left);  // Player facing direction is updated to left
        }

        else if (moveVertical >= 0.7f)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, reach) && hit.transform.tag == "wall")  // Checks if object global forwards raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.forward * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.forward, Color.white);
                //Debug.Log("Did Not Hit");

                currentMove = Vector3.forward * move * moveVertical;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward);  // Player facing direction is updated to forwards
        }

        else if (moveVertical <= -0.7f)
        {
            if (Physics.Raycast(transform.position, Vector3.back, out hit, reach) && hit.transform.tag == "wall")  // Checks if object global backwards raycast is colliding with a wall
            {
                // Draws an active ray
                Debug.DrawRay(transform.position, Vector3.back * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }
            else
            {
                // Draws an inactive ray
                Debug.DrawRay(transform.position, Vector3.back, Color.white);
                //Debug.Log("Did Not Hit");

                currentMove = Vector3.back * move * -moveVertical;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.back);  // Player facing direction is updated to backwards
        }

        transform.Translate(currentMove, Space.World);  // Player position is updated
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")  // Enemy-Player Collisions
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "Dot")  // Collisions with dot object
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            Destroy(collision.gameObject);

            if (gameObject.tag == "Player")
            {
                playerScore += 1;
            }
            else if (gameObject.tag == "Enemy")
            {
                enemyScore += 1;
            }
            Debug.Log("Player: " + playerScore + "  :  Enemy: " + enemyScore);

        }


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Holy shit dude, you killed him!");
        }
        else if (collision.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            transform.position = originalPosition;
            timeToFreeze = 3.0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "Player")  // Allows enemies/players to interact with each other
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false);

        }

        if (collision.gameObject.tag == "Dot")  // Allows enemies/players to collect dots
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false);
        }

    }

}
