using System.Collections;
using System.Collections.Generic;
using UnityEngine
using TMPro


public class sumatoriacoin : MonoBehaviour
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
    private void Ontriggerenter2D(collider2D other)
    {
        if (other.comparetag("playerprofesor"))
        {
            puntaje.sumarpuntos(cantidadpuntos);
        }
    }

    
}
