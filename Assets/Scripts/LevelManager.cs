using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SpawnPoint lastCheckPoint;

    public CharacterController character;


    

    // Start is called before the first frame update
    void Start()
    {
        character.lvlManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       
    public void CheckPoint()
    {
        
    }

    public void Respawn()
    {
        character.gameObject.SetActive(false);
        character.transform.position = lastCheckPoint.transform.position;
        character.Reborn();
        character.gameObject.SetActive(true);
    }
    
}


