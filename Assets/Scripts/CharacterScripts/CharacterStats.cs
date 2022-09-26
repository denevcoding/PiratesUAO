using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public int lifes;
    public int currentLife;

    public CharacterController pirateCharacter;

    public UIManager uiManager;


    private void Awake()
    {
        pirateCharacter = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateDamage()
    {
        if (lifes > 1)
        {
            lifes--;

            //float factor = lifes * 4 / 100;
            //Debug.Log("Factor " + factor);

            uiManager.SubstractLife(lifes, 4);
            pirateCharacter.DelayRespawn(2f);
        }
        else
        {
            uiManager.Dead();
            pirateCharacter.Dead();
          
        }       
    }


}
