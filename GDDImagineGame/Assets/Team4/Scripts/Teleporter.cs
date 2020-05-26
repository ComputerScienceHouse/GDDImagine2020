using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public static float numTeleporters;
    public int pairId;
    public int id;
    public GameObject partner;
    // Start is called before the first frame update
    void Start()
    {
        // Assigns partner value based on spawn order
        // We should change this to read teleporter pairs from a file;
        numTeleporters++;
        pairId = (int)System.Math.Ceiling(numTeleporters / 2);
        id = (int)numTeleporters;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
            case "Player":
                other.gameObject.transform.position = partner.GetComponent<Collider>().transform.position + new Vector3(0, 0.5f, 0);
                break;
            default:
                break;
        }
    }
}
