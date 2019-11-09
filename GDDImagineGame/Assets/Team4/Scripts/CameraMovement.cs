using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public Transform target2;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = ((target.position + target2.position)/2) + offset;
    }

    /*
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
    */
}
