using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float rotateSpeed;

    void Start()
    {
        transform.Rotate(0, 0, Random.Range(0, 90));

        if (minSpeed != 0 || maxSpeed != 0)
        {
            rotateSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }


    void Update()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * rotateSpeed), Space.World);
    }

}