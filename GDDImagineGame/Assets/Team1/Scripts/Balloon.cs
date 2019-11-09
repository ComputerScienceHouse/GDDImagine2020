using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Material material;
    int points;
    /// <summary>
    /// Lesser bound of balloon speed value (default: .01f)
    /// </summary>
    public float lowerSpeedRange;
    /// <summary>
    /// Upper bound of balloon speed value (default: .05f)
    /// </summary>
    public float upperSpeedRange;
    private float balloonSpeed;
    private float tempBalloonSpeed;
    private RaycastHit hit;
    CollisionManager cManage;
    GameObject temp; //Placeholder
    //Collision collision;

    public float BalloonSpeed
    {
        get{ return balloonSpeed;}
        set { balloonSpeed = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        balloonSpeed = Random.Range(lowerSpeedRange, upperSpeedRange);

        ColorSelection();
       // material = gameObject.GetComponent<Renderer>().material;
       // int randColor = Random.Range(1, 4);
       //switch (randColor)
       //{
       //    case 1:
       //        material.SetColor("red", Color.red);
       //        break;
       //    case 2:
       //        material.SetColor("blue", Color.blue);
       //        break;
       //    case 3:
       //        material.SetColor("green", Color.green);
       //        break;
       //
       //}
       //gameObject.GetComponent<Renderer>().material = material;
        points = 100;
        cManage = FindObjectOfType<CollisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        BalloonMoveDown();
        CheckBalloonRaycast();
        //BalloonCollision(collision);
        //if (cManage.AABBCollision(gameObject, temp))
        //{
        //    deleteObject();
        //}
        if (Input.GetKeyDown(KeyCode.D))
        {
            deleteObject();
        }
    }

    /// <summary>
    /// Will Move the Balloons down at a random speed
    /// </summary>
    void BalloonMoveDown()
    {
        transform.position = new Vector3(transform.position.x, (transform.position.y - balloonSpeed));
    }

    void CheckBalloonRaycast()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, .5f);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f))
        {
            if (hit.collider.tag == "Balloon")
            {
                tempBalloonSpeed = hit.collider.gameObject.GetComponent<Balloon>().BalloonSpeed;
                hit.collider.gameObject.GetComponent<Balloon>().BalloonSpeed = balloonSpeed;
                balloonSpeed = tempBalloonSpeed;
            }
        }
    }

    void ColorSelection()
    {
        int colorValue = Random.Range(1, 4);
        switch (colorValue)
        {
            case 1:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 3:
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
        }
    }

   //void BalloonCollision(Collision col)
   //{
   //    if (col.gameObject.tag == "Balloon")
   //    {
   //        tempBalloonSpeed = col.gameObject.GetComponent<Balloon>().BalloonSpeed;
   //        col.gameObject.GetComponent<Balloon>().BalloonSpeed = balloonSpeed;
   //        balloonSpeed = tempBalloonSpeed;
   //    }
   //}

    void deleteObject()
    {
        GUI_Manager GUI = FindObjectOfType<GUI_Manager>();
        GUI.AddScore(1, points);
        GameObject.Destroy(gameObject);
    }
}
