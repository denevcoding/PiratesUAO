using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed;
    public LevelManager lvlManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //luisito

    // Update is called once per frame
    void Update()
    {

        moveObstacle();

        if (transform.position.x < -13)
        {
            lvlManager.SpawnObstacle(new Vector2(12, 1));
            Destroy(this.gameObject);
        }
    }

    public void moveObstacle()
    {
        Vector3 obstaclePosition = transform.position;

        obstaclePosition.x -= speed * Time.deltaTime;

        transform.position = obstaclePosition;
    }
}
