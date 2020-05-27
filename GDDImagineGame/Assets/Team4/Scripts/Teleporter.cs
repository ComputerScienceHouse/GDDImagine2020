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
    public int coordinateX;
    public int coordinateY;
    public TeleporterType teleporterType;
    // Start is called before the first frame update
    void Start()
    {
        // Assigns partner value based on spawn order
        // We should change this to read teleporter pairs from a file;
        fromPartner = new List<GameObject>();
        numTeleporters++;
        pairId = (int)System.Math.Ceiling(numTeleporters / 2);
        id = (int)numTeleporters;
        //Debug.Log("(" + coordinateX + ", " + coordinateY + ")");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum TeleporterType
    {
        PLATFORM,
        TOP_WALL,
        BOTTOM_WALL,
        LEFT_WALL,
        RIGHT_WALL
    }

    // If a player(ally/enemy) steps on a teleport pad, they
    // are then teleported to the pad's pair, and will not be
    // teleported back
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

    //When player steps off teleport pad, they are able to teleport
    //again once they have stepped on a teleport pad
    private void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("Stepped off");
        if (teleporterType.Equals(TeleporterType.PLATFORM) || fromPartner.Contains(other.gameObject))
        {
            fromPartner.Remove(other.gameObject);
        }
    }

    private void teleport(GameObject player)
    {
        Debug.Log("Stepped On");
        if (teleporterType.Equals(TeleporterType.PLATFORM))
        {
            partner.GetComponent<Teleporter>().fromPartner.Add(player);
        }
        player.transform.position = partner.GetComponent<Transform>().transform.position + new Vector3(0, 0.5f, 0);
    }
}
