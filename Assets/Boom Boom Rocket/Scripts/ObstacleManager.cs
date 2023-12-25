using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class ObstacleManager : MonoBehaviour {
    
    
    [SerializeField] private GameObject rocket;

    [Header(" Map ")]
    [SerializeField] private float leftEnd;
    [SerializeField]private float rightEnd;


    [Space(10)] 
    [SerializeField] private GameObject pfObsCircle;
    [SerializeField] private int bigCircleNumber;
    [SerializeField] private float bigCircleSizeMin;
    [SerializeField] private float bigCircleSizeMax;
    [SerializeField] private int smallCircleNumber;
    [SerializeField] private float smallCircleSizeMin;
    [SerializeField] private float smallCircleSizeMax;

    [Space(10)]
    [SerializeField] private GameObject pfObsRectangle;
    [SerializeField] private int rectangleNumber;
    [SerializeField] private float rectangleSizeMin;
    [SerializeField] private float rectangleSizeMax;

    [Space(10)]
    [SerializeField] private GameObject pfItem;
    [SerializeField] private int itemNumber;


    [Space(10)]
    [SerializeField] private GameObject pfForceArea;
    

    [SerializeField] private List<GameObject> mapParentList;
    
    
    private float distanceToFirstMap = 5;
    private float distanceToNextMap = 10;
    private int mapIndex = 0;

    
    private void Awake()
    {
        mapParentList = new List<GameObject>();
    }

    
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GenerateNewMap();
        }
    }

    
    private void Update()
    {
        // update the map when the distance between the rocket and the map increases.
        if (mapParentList[0].transform.position.y < rocket.transform.position.y - 15)
        {
            // destroy the furthest map
            Destroy(mapParentList[0]);
            // remove from list
            mapParentList.RemoveAt(0);
            // generate new map
            GenerateNewMap();
        }
    }


    private void GenerateNewMap()
    {
        GameObject newMapParent = new GameObject();
        newMapParent.transform.position = new Vector3(0, distanceToFirstMap + mapIndex * distanceToNextMap, 0);
       
        //generate various forms of obstacle
        GenerateObstacle(pfObsCircle, bigCircleNumber, bigCircleSizeMin, bigCircleSizeMax,newMapParent);
        GenerateObstacle(pfObsCircle, smallCircleNumber, smallCircleSizeMin, smallCircleSizeMax,newMapParent);
        GenerateObstacle(pfObsRectangle, rectangleNumber, rectangleSizeMin, rectangleSizeMax,newMapParent );
        GenerateObstacle(pfForceArea, 1, 1f, 1f,newMapParent);
        GenerateObstacle(pfItem, itemNumber, 1f, 1f,newMapParent);
        
        // increase mapIndex
        mapIndex++;
        
        //add to list
        mapParentList.Add(newMapParent);
    }


    void GenerateObstacle(GameObject prefab, int number, float minSize, float maxSize, GameObject newMapParent)
    {
        for (int i = 0; i < number; i++)
        {
            // calculate position of new obstacle
            float posX = Random.Range(leftEnd, rightEnd);
            float posY = Random.Range(
                distanceToFirstMap + mapIndex * distanceToNextMap + 1, 
                distanceToFirstMap + (mapIndex + 1) * distanceToNextMap - 1
                );
            
            GameObject newObj = Instantiate(prefab, new Vector2(posX, posY), Quaternion.identity);
            
            float randomSize = Random.Range(minSize, maxSize);
            
            // add random size, angle
            if (prefab == pfObsRectangle)
            {
                newObj.transform.localScale = new Vector2(newObj.transform.localScale.x * randomSize, newObj.transform.localScale.y * randomSize );
            }
            else if (prefab == pfObsCircle)
            {
                newObj.transform.localScale = new Vector2(newObj.transform.localScale.x * randomSize, newObj.transform.localScale.y * randomSize);
            }
            else if (prefab == pfForceArea)
            {
                newObj.transform.Rotate(0, 0, Random.Range(0, 360));
            }
            else if (prefab == pfItem)
            {

            }

            newObj.transform.SetParent(newMapParent.transform);
        }
    }




}

