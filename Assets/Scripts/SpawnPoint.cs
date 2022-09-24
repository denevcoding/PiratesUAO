using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public LevelManager lvlManager;
    public bool used;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used == true)
            return;

        if (collision.gameObject.GetComponent<CharacterController>())
        {            
            lvlManager.lastCheckPoint = this;
            used = true;
        }
        
    }
}
