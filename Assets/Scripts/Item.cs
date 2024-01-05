using UnityEngine;

public class Item : MonoBehaviour {


    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float moveSpeed;
    


    void Start()
    {
        if (minSpeed != 0 || maxSpeed != 0) 
            moveSpeed = Random.Range(minSpeed, maxSpeed);
    }


    void Update()
    {
        if (GamePlayManager.Instance.IsGameOvered()) return;
        
        MoveDown();
    }


    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}