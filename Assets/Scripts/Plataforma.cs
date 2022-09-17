using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float TimeToDestroy;
    public float Counter;

    public bool counting;

    // Start is called before the first frame update
    void Start()
    {
        Counter = 0f;
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
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
    }



}
