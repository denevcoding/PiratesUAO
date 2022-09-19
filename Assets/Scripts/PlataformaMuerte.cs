using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMuerte : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterController player = collision.gameObject.GetComponent<CharacterController>();

        if (player)
        {
            player.Dead();
            //player.state = PirateState.Dead;
            Debug.Log(collision.gameObject.name);
            
        }

    }
}
