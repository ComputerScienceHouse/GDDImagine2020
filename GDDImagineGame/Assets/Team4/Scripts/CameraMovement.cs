using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 camStart;
    private Vector3 camEnd;
    private Vector3 lightStart;
    private Vector3 lightEnd;
    // Light Prefab
    public GameObject LightPrefab;

    // This will be all of the objects that can be played in the room
    private GameObject[] players;
    private GameObject[] enemies;

    private GameObject lightObject;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        lightObject = Instantiate(LightPrefab);

        float x = 0, z = 0;
        foreach (GameObject player in players)
        {
            x += player.transform.position.x;
            z += player.transform.position.z;
        }

        foreach (GameObject enemy in enemies)
        {
            x += enemy.transform.position.x;
            z += enemy.transform.position.z;
        }

        transform.position = new Vector3(x / 4, 40, z / 4 - 50);
    }

    // Update is called once per frame
    void Update()
    {
        // Now to get the average position between all of the players
        float x = 0, z = 0;
        foreach (GameObject player in players)
        {
            x += player.transform.position.x;
            z += player.transform.position.z;
        }

        foreach (GameObject enemy in enemies)
        {
            x += enemy.transform.position.x;
            z += enemy.transform.position.z;
        }

        // Define start and end vectors here
        camStart = transform.position;
        lightStart = lightObject.transform.position;
        camEnd = new Vector3(x / 4, 40, z / 4 - 50);
        lightEnd = new Vector3(x /4, 40, z / 4);

        // Move the camera and light based on this
        //transform.position = new Vector3(x / 4, 40, z / 4 - 50);
        //lightObject.transform.position = new Vector3(x / 4, 40, z / 4);

        transform.position = Vector3.Lerp(camStart, camEnd, 0.05f);
        lightObject.transform.position = Vector3.Lerp(lightStart, lightEnd, 0.05f);
    }
}
