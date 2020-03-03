using System.IO;
using UnityEngine;
using glipglop;
using System.Collections.Generic;

public class DynamicLevelMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject WallPrefab;

    [SerializeField]
    public GameObject FloorPrefab;

    [SerializeField]
    public GameObject PlayerPrefab;

    [SerializeField]
    public GameObject EnemyPrefab;

    [SerializeField]
    public GameObject DotPrefab;

    private GameObject[,] objects;
    private GameObject floor;
    private DeviceManager manager;

    public int scale;
    public string roomName;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, List<string>> devices = new Dictionary<string, List<string>>
        {
            {
                "D0",
                new List<string>()
                {
                    "P0",
                    "P1",
                    "P2",
                    "P3",
                }
            }
        };

        manager = new DeviceManager(devices);
        manager.AddPressed(new PressedDel(SlowPlayers), "P0", "D0");
        manager.AddReleased(new ReleasedDel(NormalSpeedPlayers), "P0", "D0");

        //Try catch 
        try
        {
            StreamReader roomReader = new StreamReader($"{Application.dataPath}/Team4/Scripts/{roomName}");

            string line;
            int width = int.Parse(roomReader.ReadLine());
            int height = int.Parse(roomReader.ReadLine());
            int currentController = 0;

            objects = new GameObject[width, height];

            for (int j = 0; j < height; j++)
            {
                line = roomReader.ReadLine();
                for (int i = 0; i < width; i++)
                {
                    switch (line[i])
                    {
                        // Add a wall to the game
                        case 'X':
                            objects[i, j] = Instantiate(WallPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            break;
                        // Create a floor piece
                        case '.':
                            objects[i, j] = Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            break;
                        // Create a dot piece with a floor piece under it
                        case 'D':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(DotPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            break;
                        // Add an enemy to the game and a floor under them
                        case 'E':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(EnemyPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            PlayerMovement enemy = FindObjectOfType<PlayerMovement>();

                            enemy.joystickNumber = currentController;
                            currentController++;
                            break;
                        // Add a player to the scene
                        case 'P':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(PlayerPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            PlayerMovement player = FindObjectOfType<PlayerMovement>();

                            player.joystickNumber = currentController;
                            currentController++;
                            break;
                        // Big uhoh
                        default:
                            break;
                    }
                }
            }

            roomReader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log(Application.dataPath);
            Debug.Log(e.Message);
            //TODO: Here we should just make a default room
        }
    }

    void SlowPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
        foreach (GameObject player in players)
        {
            PlayerMovement comp = player.GetComponent<PlayerMovement>();

            comp.speed = comp.defaultSpeed - 5;
        }
    }

    void NormalSpeedPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayerMovement comp = player.GetComponent<PlayerMovement>();

            comp.speed = comp.defaultSpeed;
        }
    }
}
