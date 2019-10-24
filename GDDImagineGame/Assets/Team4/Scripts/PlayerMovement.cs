using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Transform tf;
    private Rigidbody rb;

    public GameObject player;

    public float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit hit;
        Ray myRay = new Ray(transform.position, new Vector3(0.01f, 0, 0));

        if(Physics.Raycast(myRay, out hit, transform.position[0] + 0.01f))
        {
            if(hit.collider.tag == "wall")
            {
                tf.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
        }
        */

        float move = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, tf.position, move);

        if (Input.GetKey(KeyCode.D))
        {
            tf.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        else if (Input.GetKey(KeyCode.A))
        {
            tf.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }

        else if (Input.GetKey(KeyCode.W))
        {
            tf.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        
        else if (Input.GetKey(KeyCode.S))
        {
            tf.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "wall")
        {
            Debug.Log("Hit Sphere");
        }
    }
}
