using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public static float numTeleporters;
    public int pairId;
    public int id;
    public GameObject partner;
    public List<GameObject> fromPartner;
    //public int coordinateX;
    //public int coordinateY;
    // Start is called before the first frame update
    void Start()
    {
        // Assigns partner value based on spawn order
        // We should change this to read teleporter pairs from a file;
        fromPartner = new List<GameObject>();
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
        Debug.Log(other);
        if (!fromPartner.Contains(other.gameObject)) {
            switch (other.gameObject.tag)
            {
                case "Enemy":
                case "Player":
                    teleport(other.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fromPartner.Remove(other.gameObject);
    }

    private void teleport(GameObject player)
    {
        partner.GetComponent<Teleporter>().fromPartner.Add(player);
        player.transform.position = partner.GetComponent<Collider>().transform.position + new Vector3(0, 0.5f, 0);
    }
}
