using System.IO;
using UnityEngine;

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

    private GameObject[,] objects;
    private GameObject floor;

    public int scale;
    public string roomName;

    // Start is called before the first frame update
    void Start()
    {
        //Try catch 
        try
        {
            StreamReader roomReader = new StreamReader($"{Application.dataPath}/Team4/Scripts/{roomName}");

            string line;
            int width = int.Parse(roomReader.ReadLine());
            int height = int.Parse(roomReader.ReadLine());

            objects = new GameObject[height, width];

            for (int i = 0; i < height; i++)
            {
                line = roomReader.ReadLine();
                for (int j = 0; j < width; j++)
                {
                    switch (line[j])
                    {
                        // Add a wall to the game
                        case 'X':
                            objects[i, j] = Instantiate(WallPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            break;
                        // Create a floor piece
                        case '.':
                            objects[i, j] = Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            break;
                        // Add an enemy to the game and a floor under them
                        case 'E':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(EnemyPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            break;
                        // Add a player to the scene
                        case 'P':
                            Instantiate(FloorPrefab, new Vector3((scale * i), 0, (scale * j)), Quaternion.identity);
                            objects[i, j] = Instantiate(PlayerPrefab, new Vector3((scale * i), 0.5f, (scale * j)), Quaternion.identity);
                            break;
                        // Big uhoh
                        default:
                            break;
                    }
                }
            }

            roomReader.Close();
        }
        catch
        {
            Debug.Log(Application.dataPath);
            Debug.Log("Could not find the file");
            //TODO: Here we should just make a default room
        }
    }

    // Update is called once per frame
    void Update()
    {
         // Not sure if we will ever need this or not
    }
}
