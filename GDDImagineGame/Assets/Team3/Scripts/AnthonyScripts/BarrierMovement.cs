using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{
    float camSpeed = 2f;
    public int ZOffset;
    public int XOffset;
    public GameObject cam;
    public GameObject barrier;

    // Update is called once per frame

    void Update()
    {
        barrier.transform.position = new Vector3(cam.transform.position.x + XOffset, cam.transform.position.y-10, cam.transform.position.z + (camSpeed * Time.deltaTime) + ZOffset);
    }
}
