using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Transform tf;
    private Rigidbody rb;
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
        float move = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, tf.position, move);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            tf.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            tf.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            tf.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            tf.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
    }
}
