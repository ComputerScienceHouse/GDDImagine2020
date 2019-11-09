using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Material material;
    int points;
    CollisionManager cManage;
    GameObject temp; //Placeholder
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        int randColor = Random.Range(1, 4);
        switch (randColor)
        {
            case 1:
                material.SetColor("red", Color.red);
                break;
            case 2:
                material.SetColor("blue", Color.blue);
                break;
            case 3:
                material.SetColor("green", Color.green);
                break;

        }
        points = 100;
        cManage = FindObjectOfType<CollisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (cManage.AABBCollision(gameObject, temp))
        //{
        //    deleteObject();
        //}
        if (Input.GetKeyDown(KeyCode.D))
        {
            deleteObject();
        }
    }

    void deleteObject()
    {
        GUI_Manager GUI = FindObjectOfType<GUI_Manager>();
        GUI.AddScore(1, points);
        GameObject.Destroy(gameObject);
    }
}
