using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject hearthLife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubstractLife(int currentLifes, int totalLifes)
    {
        Vector3 newScale = hearthLife.transform.localScale;
        float factor = currentLifes * 1f / totalLifes;       
        newScale *= factor;
        hearthLife.transform.localScale = newScale;
    }

    public void Dead()
    {
        hearthLife.SetActive(false);
        Invoke("ReloadScene", 3f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Test level Santiago Backup", LoadSceneMode.Single);
    }

}
