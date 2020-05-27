using System.IO;
using UnityEngine;
//using glipglop;
using System.Collections.Generic;

public class DynamicLevelMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject WallPrefab;

    [SerializeField]
    private GameObject FloorPrefab;

    [SerializeField]
    private GameObject BarrierPrefab;

    [SerializeField]
    private GameObject TeleporterPrefab;

    [SerializeField]
    private GameObject TopTeleporterPrefab;

    [SerializeField]
    private GameObject BottomTeleporterPrefab;

    [SerializeField]
    private GameObject LeftTeleporterPrefab;

    [SerializeField]
    private GameObject RightTeleporterPrefab;

    [SerializeField]
    public GameObject PlayerPrefab;

    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private GameObject DotPrefab;

    [SerializeField]
    private GameObject KillConfirmedPrefab;

    public GameObject[,] objects;
    private IDictionary<int, List<GameObject>> teleporters;
    private char[,] teleporterChars;
    //private GameObject floor;
    //private DeviceManager manager;

    public int scale;
    public string roomName;
    private System.Random rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        //manager = new DeviceManager();
        //manager.AddPressed(new PressedDel(SlowPlayers), "D0", "P0");
        //manager.AddReleased(new ReleasedDel(NormalSpeedPlayers), "D0", "P0");

        //Try catch 
        try
        {
            StreamReader roomReader = new StreamReader($"{Application.dataPath}/Team4/rooms/{roomName}/room.txt");

            string line;
            int width = int.Parse(roomReader.ReadLine());
            int height = int.Parse(roomReader.ReadLine());
            int currentController = 0;

            objects = new GameObject[width, height];
            
            teleporters = new Dictionary<int, List<GameObject>>();
            teleporterChars = new char[width, height];

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
                            objects[i, j].GetComponent<Score>().ScoreVal = 1;
                            break;
                        // Add an enemy to the game and a floor under them
                        case 'E':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(EnemyPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            Player enemy = FindObjectOfType<EnemyPlayer>();

                            enemy.joystickNumber = currentController;
                            currentController++;
                            break;
                        // Add a player to the scene
                        case 'P':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(PlayerPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            Player player = FindObjectOfType<AllyPlayer>();

                            player.joystickNumber = currentController;
                            currentController++;
                            break;
                        // Add Ally Barrier to the scene
                        case 'a':
                            objects[i, j] = Instantiate(BarrierPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j].GetComponent<Barrier>().setAlliance(Player.Alliance.ALLY);
                            break;
                        // Add Enemy Barrier to the scene
                        case 'e':
                            objects[i, j] = Instantiate(BarrierPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j].GetComponent<Barrier>().setAlliance(Player.Alliance.ENEMY);
                            break;
                        // Add wall teleporting pad
                        case 't':
                            // Ignore for the time being
                            // Teleporters are placed later
                            teleporterChars[i, j] = 't';
                            break;
                        // Add floor teleporting pad
                        /*
                        case 'T':
                            objects[i, j] = Instantiate(TeleporterPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j].GetComponent<Teleporter>().coordinateX = i;
                            objects[i, j].GetComponent<Teleporter>().coordinateY = j;
                            int pairId = objects[i, j].GetComponent<Teleporter>().pairId;
                            if (!teleporters.ContainsKey(pairId))
                            {
                                teleporters.Add(pairId, new List<GameObject>(2));
                            }
                            // list is created if it doesnt already exist
                            if (teleporters[pairId].Count < 2)
                            {
                                teleporters[pairId].Add(objects[i, j]);
                            }
                            break;
                            */
                        // Big uhoh
                        default:
                            objects[i, j] = new GameObject();
                            break;
                    }
                }
            }

            roomReader.Close();

            spawnTeleporters();
            //InitTeleporters();
        }
        catch (System.Exception e)
        {
            Debug.Log(Application.dataPath);
            Debug.Log(e.Message);
            //TODO: Here we should just make a default room
        }
    }

    public void spawnTeleporters()
    {
        StreamReader teleportersReader = new StreamReader($"{Application.dataPath}/Team4/rooms/{roomName}/teleporters.txt");

        string line;
        while ((line = teleportersReader.ReadLine()) != null)
        {
            string[] teleporterInfo;
            // Checks if line is a comment
            if (line.Length != 0)
            {
                // '#' denotes a comment in the file
                if (line[0].Equals('#'))
                {
                    Debug.Log("This is a comment");
                    continue;
                }
            } 
            else
            {
                continue;
            }
            teleporterInfo = line.Split(' ');
            // Checks if teleporterInfo is valid
            if (!checkTeleporterInfo(teleporterInfo))
            {
                // Teleporter pair info was invalid
                // Moving onto the next teleporter pair
                // room file needs fixing! (We should create an error log file)
                continue;
            }
            List<GameObject> pair = new List<GameObject>(2);
            GameObject t1;
            GameObject t2;
            int x1 = System.Int32.Parse(teleporterInfo[2]);
            int x2 = System.Int32.Parse(teleporterInfo[4]);
            int y1 = System.Int32.Parse(teleporterInfo[3]);
            int y2 = System.Int32.Parse(teleporterInfo[5]);
            switch (teleporterInfo[1])
            {
                case "H":
                    t1 = Instantiate(LeftTeleporterPrefab, new Vector3((scale * x1), 0, (scale * y1)), Quaternion.identity);
                    t1.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.LEFT_WALL;
                    t2 = Instantiate(RightTeleporterPrefab, new Vector3((scale * x2), 0, (scale * y2)), Quaternion.identity);
                    t2.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.RIGHT_WALL;
                    break;
                case "V":
                    t1 = Instantiate(TopTeleporterPrefab, new Vector3((scale * x1), 0, (scale * y1)), Quaternion.identity);
                    t1.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.TOP_WALL;
                    t2 = Instantiate(BottomTeleporterPrefab, new Vector3((scale * x2), 0, (scale * y2)), Quaternion.identity);
                    t2.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.BOTTOM_WALL;
                    break;
                case "P":
                    t1 = Instantiate(TeleporterPrefab, new Vector3((scale * x1), 0, (scale * y1)), Quaternion.identity);
                    t1.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.PLATFORM;
                    t2 = Instantiate(TeleporterPrefab, new Vector3((scale * x2), 0, (scale * y2)), Quaternion.identity);
                    t2.GetComponent<Teleporter>().teleporterType = Teleporter.TeleporterType.PLATFORM;
                    break;
                default:
                    // will never get here due to prior fail-safe check
                    t1 = new GameObject();
                    t2 = new GameObject();
                    break;
            }
            objects[x1, y1] = t1;
            objects[x2, y2] = t2;
            pair.Add(t1);
            pair.Add(t2);
            teleporters.Add(System.Int32.Parse(teleporterInfo[0]), pair);
            Debug.Log(line);
        }
        InitTeleporters();

        teleportersReader.Close();
    }

    private bool checkTeleporterInfo(string[] teleporterInfo)
    {
        try
        {
            if (teleporterInfo.Length != 6)
                throw new System.Exception("Invalid teleporter info");
            int pairId = System.Int32.Parse(teleporterInfo[0]);
            string orientation = teleporterInfo[1];
            int x1 = System.Int32.Parse(teleporterInfo[2]);
            int x2 = System.Int32.Parse(teleporterInfo[4]);
            int y1 = System.Int32.Parse(teleporterInfo[3]);
            int y2 = System.Int32.Parse(teleporterInfo[5]);

            if (!(orientation.Equals("H") || orientation.Equals("V") || orientation.Equals("P")) 
                || teleporterChars[x1, y1] != 't' || teleporterChars[x2, y2] != 't')
                throw new System.Exception("Invalid orientation type");
            return true;
        } 
        catch (System.Exception e)
        {
            return false;
        }
    }
    public void InitTeleporters()
    {
        foreach (int pairId in teleporters.Keys)
        {
            List<GameObject> pairList = teleporters[pairId];
            if (pairList != null && pairList.Count == 2)
            {
                GameObject t1 = pairList[0];
                GameObject t2 = pairList[1];
                t1.GetComponent<Teleporter>().partner = t2;
                t2.GetComponent<Teleporter>().partner = t1;
            }
        }
    }

    public void RemoveObject(GameObject gameObject)
    {
        int x = (int)gameObject.transform.position.x;
        int z = (int)gameObject.transform.position.z;

        objects[x, z] = null;

        Destroy(gameObject);
    }

    public int KillConfirmed(Vector3 deadPosition, int playerScore)
    {

        List<Vector3> freePoints = new List<Vector3>();

        for (int x = 0; x < objects.GetLength(0); x++)
        {
            for (int y = 0; y < objects.GetLength(1); y++)
            {
                if(objects[x, y] == null)
                {
                    freePoints.Add(new Vector3(x, 0.5f, y));
                } else if (objects[x, y].tag == "Floor")
                {
                    freePoints.Add(new Vector3(0, 0.5f, 0) + objects[x, y].transform.position);
                }
            }
        }

        Player[] players = FindObjectsOfType<Player>();

        foreach (Vector3 vector in freePoints)
        {
            if (vector.x == deadPosition.x && vector.z == deadPosition.z)
            {
                freePoints.Remove(vector);
                break;
            }
        }

        object removeVect = null;
        foreach (Player player in players)
        {
            foreach(Vector3 vector in freePoints)
            {
                if (vector.x == player.gameObject.transform.position.x && vector.z == player.gameObject.transform.position.z)
                {
                    removeVect = vector;
                }
            }
            if (removeVect != null)
            {
                freePoints.Remove((Vector3)removeVect);
                removeVect = null;
            }
        }
        if (freePoints.Count == 0)
        {
            return 0;
        }
        int point = rand.Next(0, freePoints.Count);

        objects[(int)freePoints[point].x, (int)freePoints[point].z] = Instantiate(KillConfirmedPrefab, freePoints[point], Quaternion.identity);
        objects[(int)freePoints[point].x, (int)freePoints[point].z].GetComponent<Score>().ScoreVal = (playerScore / 2);

        return playerScore / 2;

    }

    void SlowPlayers()
    {
        Debug.Log("Slowing players");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
        foreach (GameObject player in players)
        {
            Player comp = player.GetComponent<Player>();

            comp.speed = 0;//comp.defaultSpeed - 5;
        }
    }

    void NormalSpeedPlayers()
    {
        Debug.Log("Normalize players");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Player comp = player.GetComponent<Player>();

            comp.speed = comp.defaultSpeed;
        }
    }
}
