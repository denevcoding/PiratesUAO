using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float TimeToDestroy;
    public float Counter;
    private float timeToRespawn;

    public bool counting;
    Vector3 initialPosition;

    public LevelManager lvlManager;

    // Start is called before the first frame update
    void Start()
    {
        Counter = 0f;
        timeToRespawn = 3f;
        initialPosition = transform.position;

        lvlManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleDestroyCounter();
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            Debug.Log(collision.gameObject.name);
            counting = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            Debug.Log(collision.gameObject.name);
            counting = true;
        }
    }


    public void HandleDestroyCounter()
    {
        if (counting == true)
        {
            Counter += Time.deltaTime;

            if (Counter >= TimeToDestroy)
            {
                Debug.Log("Me destruyo");
                lvlManager.RespawnPlatform(this.gameObject, initialPosition, timeToRespawn);
                counting = false;
                Counter = 0f;
                this.gameObject.SetActive(false);
               
                //Destroy(this.gameObject);
            }
        }
    }


    



}
