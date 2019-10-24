using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCast : MonoBehaviour
{
    public GameObject player;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8; // I don't understand what this does
        layerMask = ~layerMask; // I don't understand what this does
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), out hit, 1, layerMask)) // W
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), Color.white);
            Debug.Log("Did Not Hit");
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out hit, 1, layerMask)) // S
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), Color.white);
            Debug.Log("Did Not Hit");
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out hit, 1, layerMask)) // D
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), Color.white);
            Debug.Log("Did Not Hit");
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out hit, 1, layerMask)) // A
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), Color.white);
            Debug.Log("Did Not Hit");
        }
    }
}
