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

            floor = Instantiate(FloorPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);

            int offset = (scale / 2) - (width * scale) /2;

            for (int i = 0; i < height; i++)
            {
                line = roomReader.ReadLine();
                for (int j = 0; j < width; j++)
                {
                    switch (line[j])
                    {
                        // Add a wall to the game
                        case 'X':
                            objects[i, j] = Instantiate(WallPrefab, new Vector3((scale * i) + offset, 0.5f, (scale * j) + offset), Quaternion.identity);
                            break;
                        // Skip this spot it's open
                        case 'O':
                            break;
                        // Add an enemy to the game
                        case 'E':
                            objects[i, j] = Instantiate(EnemyPrefab, new Vector3(scale * i, 0.5f, scale * j), Quaternion.identity);
                            break;
                        // Add a player to the scene
                        case 'P':
                            objects[i, j] = Instantiate(PlayerPrefab, new Vector3(scale * i, 0.5f, scale * j), Quaternion.identity);
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
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
