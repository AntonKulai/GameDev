using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    
    
    [SerializeField] private GameObject target;
    [SerializeField] private float targetYOffset;

    private Vector3 velocity = Vector3.zero;
    private float lastYPosition;
    
    void LateUpdate()
    {
        float smoothTime = 0.3F;
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + targetYOffset, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (transform.position.y < lastYPosition)
            transform.position = new Vector3(transform.position.x, lastYPosition, -10);
        else
            lastYPosition = transform.position.y;
        
    }

}