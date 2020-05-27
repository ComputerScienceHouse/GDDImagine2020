using UnityEngine;

public abstract class Player : MonoBehaviour
{  
    public float speed;
    public float defaultSpeed;
    public float timeToFreeze;

    private Vector3 currentMove;
    protected Vector3 originalPosition;

    public int joystickNumber;

    public int localScore;

    protected PlayerState playerState;
    protected PlayerState lastPlayerState;
    public Alliance alliance;

    protected void Start()
    {
        localScore = 0;

        playerState = PlayerState.ALIVE;
        lastPlayerState = PlayerState.ALIVE;

        speed = 10.0f;
        defaultSpeed = 10.0f;
        currentMove = Vector3.zero;
        this.originalPosition = this.gameObject.transform.position;
        timeToFreeze = 0.0f;
    }
 
    protected void FixedUpdate()
    {
        if (timeToFreeze > 0)
        {
            //Sets object material to dead texture
            if (lastPlayerState.Equals(PlayerState.ALIVE))
            {
                gameObject.GetComponent<MeshRenderer>().material = setMaterial(PlayerState.DEAD);
            }
            
            timeToFreeze -= Time.deltaTime;
        } 
        else
        {
            //Sets object material to alive texture
            if (lastPlayerState.Equals(PlayerState.DEAD))
            {
                gameObject.GetComponent<MeshRenderer>().material = setMaterial(PlayerState.ALIVE);
            }
            playerControl();
        }
    }

    public enum Alliance
    {
        ALLY,
        ENEMY
    }
    protected enum PlayerState
    {
        DEAD,
        ALIVE
    }

    //Sets lastPlayerState to current, and current to PlayerState param
    protected void setPlayerState(PlayerState newState)
    {
        if (!playerState.Equals(newState))
        {
            lastPlayerState = playerState;
            playerState = newState;
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

        setPlayerState(PlayerState.ALIVE);
        Shoot(num); // Abstract method

        if (moveHorizontal >= 0.7f)
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, reach) 
                && isWallCollision(hit))  // Checks if object global right raycast is colliding with a wall
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
            if (Physics.Raycast(transform.position, Vector3.left, out hit, reach) && isWallCollision(hit))  // Checks if object global left raycast is colliding with a wall
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
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, reach) && isWallCollision(hit))  // Checks if object global forwards raycast is colliding with a wall
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
            if (Physics.Raycast(transform.position, Vector3.back, out hit, reach) && isWallCollision(hit))  // Checks if object global backwards raycast is colliding with a wall
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

    private bool isWallCollision(RaycastHit hit)
    {
        return hit.transform.tag.Equals("wall") || (hit.transform.tag.Equals("Barrier") 
            && !hit.collider.GetComponent<Barrier>().alliance.Equals(this.alliance));
    }

    // Death and scoring vvvv

    protected void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Dot":
                FindObjectOfType<DynamicLevelMaker>().RemoveObject(collider.gameObject);
                //Score.setPlayerScore(gameObject, 1);
                setPlayerScore(1);
                //Debug.Log(name + "score: " + localScore);
                break;
            default:
                break;
        }
    }

    protected void KillConfirm(Collider collider)
    {
        // Get point value of KillConfirm object
        int scoreVal = collider.gameObject.GetComponent<Score>().ScoreVal;
        
        // Remove KillConfirm object from the collection of KillConfirm objects
        FindObjectOfType<DynamicLevelMaker>().RemoveObject(collider.gameObject);

        // Increment player score by said point value
        //Score.setPlayerScore(gameObject, scoreVal);
        setPlayerScore(scoreVal);
    }

    /* Handles player behavior on death. Player loses a number of
     * points based on their current score. They are respawned and
     * immobilized for 3 seconds.
     */
    public void InitDeath()
    {
        setPlayerState(PlayerState.DEAD);

        // Gets a KillConfirmed object score value based on player's current score
        int scoreVal = FindObjectOfType<DynamicLevelMaker>().KillConfirmed(transform.position, localScore);

        // Deducts score of player that was just killed
        //Score.setPlayerScore(gameObject, -scoreVal);
        setPlayerScore(-scoreVal);

        // Adds half of players score when killed to killer player
        //Score.setPlayerScore(collider.gameObject, scoreVal);

        // Respawns player
        transform.position = originalPosition;
        
        // Death timer
        timeToFreeze = 3.0f;
    }

    protected abstract Material setMaterial(PlayerState id);

    protected abstract void Shoot(string controllerNum);

    public void setPlayerScore(int score)
    {
        localScore += score; // Increments player's personal score
        setTeamScore(score);
    }

    public abstract void setTeamScore(int score);
}
