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
    public GameObject PlayerPrefab;

    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private GameObject DotPrefab;

    [SerializeField]
    private GameObject KillConfirmedPrefab;

    private GameObject[,] objects;
    private GameObject floor;
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
                        // Big uhoh
                        default:
                            objects[i, j] = new GameObject();
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
