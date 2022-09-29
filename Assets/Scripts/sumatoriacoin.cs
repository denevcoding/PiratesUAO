using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SumatoriaCoin : MonoBehaviour
{

    private float cantidadpuntos;
    private float puntaje;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Ontriggerenter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("playerprofesor"))
        {
            //puntaje.sumarpuntos(cantidadpuntos);
        }
    }



    
}
