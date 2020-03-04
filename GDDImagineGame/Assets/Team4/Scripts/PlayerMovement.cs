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

    public int localScore;

    void Start()
    {
        playerScore = 0;
        enemyScore = 0;

        localScore = 0;

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

    void playerControl() 
    {
        string num = joystickNumber.ToString();

        float moveHorizontal = Input.GetAxisRaw($"LeftJoystickX_P{num}");
        float moveVertical = Input.GetAxisRaw($"LeftJoystickY_P{num}") * -1;

        int layerMask = 0; // This can be zero, it then stops the weird collision thing
        RaycastHit hit;

        float move = speed * Time.deltaTime;

        float reach = 0.51f;

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

    // Death and scoring vvvv

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")  // Enemy-Player Collisions
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "Dot")  // Collisions with dot object
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());

            FindObjectOfType<DynamicLevelMaker>().RemoveObject(collision.gameObject);

            Score.setPlayerScore(gameObject, 1);

        }

        if (collision.gameObject.tag == "KillConfirm")
        {
            int scoreVal = collision.gameObject.GetComponent<Score>().ScoreVal;
            FindObjectOfType<DynamicLevelMaker>().RemoveObject(collision.gameObject);
            Score.setPlayerScore(gameObject, scoreVal);
        }


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Holy shit dude, you killed him!");
        }
        else if (collision.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            int scoreVal = FindObjectOfType<DynamicLevelMaker>().KillConfirmed(transform.position, localScore);
            Score.setPlayerScore(gameObject, -scoreVal);
            Score.setPlayerScore(collision.gameObject, scoreVal);
            transform.position = originalPosition;
            timeToFreeze = 3.0f;
        }

        Debug.Log("Player: " + playerScore + "  :  Enemy: " + enemyScore);
        Debug.Log("Local " + (gameObject.tag == "Player" ? "Player" : "Enemy") + " Score: " + localScore);
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy" && gameObject.tag == "Player") 
            || collision.gameObject.tag == "Dot" 
            || collision.gameObject.tag == "KillConfirm")  // Allows enemies/players to interact with each other
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false);
        }
    }
}
