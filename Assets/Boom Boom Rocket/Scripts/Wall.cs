using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField] private GameObject rocket;
    
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    

    void LateUpdate()
    {
        // the wall follows rocket
        FollowRocket();
    }

    
    private void FollowRocket() 
    {
        leftWall.transform.position = new Vector2(leftWall.transform.position.x, rocket.transform.position.y);
        rightWall.transform.position = new Vector2(rightWall.transform.position.x, rocket.transform.position.y);
    }
    
}
