using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] obstacles;
  


    // Start is called before the first frame update
    void Start()
    {
        float x = 12;
        float y = Random.Range(-3 , 3);

        Vector2 obstaclePos = new Vector2(x, y);

        SpawnObstacle(obstaclePos);
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            
        }
    }


   

    public void SpawnObstacle(Vector2 pos)
    {
        int obstacleIndex = (int)Random.Range(0, 2);

        GameObject obstacle = Instantiate(obstacles[obstacleIndex], pos, Quaternion.identity);

        obstacle.GetComponent<ObstacleMovement>().speed = 5f;
        obstacle.GetComponent<ObstacleMovement>().lvlManager = this;
    } 
}


