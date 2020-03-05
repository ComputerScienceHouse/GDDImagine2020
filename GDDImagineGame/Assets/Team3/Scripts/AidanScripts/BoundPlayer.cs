using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(pos).x, transform.position.y, Camera.main.ViewportToWorldPoint(pos).z);
    }
}
