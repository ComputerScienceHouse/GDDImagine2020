using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Material material;
    int points;
    public Material colorOption1;
    public Material colorOption2;
    public Material colorOption3;
    /// <summary>
    /// Lesser bound of balloon speed value (default: .01f)
    /// </summary>
    public float lowerDragRange;
    /// <summary>
    /// Upper bound of balloon speed value (default: .05f)
    /// </summary>
    public float upperDragRange;
    //private float balloonSpeed;
    private float TempBalloonDrag;
    private RaycastHit hit;
    CollisionManager cManage;
    GameObject temp; //Placeholder
    //Collision collision;

    //public float BalloonSpeed
    //{
    //    get{ return balloonSpeed;}
    //    set { balloonSpeed = value; }
    //}


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().drag = Random.Range(lowerDragRange, upperDragRange);

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
        
       // BalloonMoveDown();
        //CheckBalloonRaycast();
            
        //if (cManage.AABBCollision(gameObject, temp))
        //{
        //    deleteObject();
        //}
        if (Input.GetKeyDown(KeyCode.D))
        {
            deleteObject();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        BalloonCollision(collision);
    }


    /// <summary>
    /// Will Move the Balloons down at a random speed
    /// </summary>
    //void BalloonMoveDown()
    //{
    //    transform.position = new Vector3(transform.position.x, (transform.position.y - balloonSpeed));
    //}
    //
    //void CheckBalloonRaycast()
    //{
    //    Debug.DrawRay(new Vector3(transform.position.x,transform.position.y - .3f, 0), Vector3.down, Color.red, .2f);
    //    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - .3f, 0), Vector3.down, out hit, .2f))
    //    {
    //        if (hit.collider.tag == "Balloon")
    //        {
    //            tempBalloonSpeed = hit.collider.gameObject.GetComponent<Balloon>().BalloonSpeed;
    //            hit.collider.gameObject.GetComponent<Balloon>().BalloonSpeed = balloonSpeed;
    //            balloonSpeed = tempBalloonSpeed;
    //        }
    //    }
    //}

    void ColorSelection()
    {
        int colorValue = Random.Range(1, 4);
        switch (colorValue)
        {
            case 1:
                GetComponent<MeshRenderer>().material = colorOption1;
                break;
            case 2:
                GetComponent<MeshRenderer>().material = colorOption2;
                break;
            case 3:
                GetComponent<MeshRenderer>().material = colorOption3;
                break;
        }
    }

   void BalloonCollision(Collision col)
   {
       if (col.gameObject.tag == "Balloon")
       {
            TempBalloonDrag = col.gameObject.GetComponent<Rigidbody>().drag;
           col.gameObject.GetComponent<Rigidbody>().drag = GetComponent<Rigidbody>().drag;
           GetComponent<Rigidbody>().drag = TempBalloonDrag;
       }
       if(col.gameObject.tag == "Dart")
        {
            Debug.Log("Dart Collision");
            deleteObject();
        }
   }

    void deleteObject()
    {
        //GUI_Manager GUI = FindObjectOfType<GUI_Manager>();
        //GUI.AddScore(1, points);
        Debug.Log("destroy");
        GameObject.Destroy(gameObject);
    }
}
