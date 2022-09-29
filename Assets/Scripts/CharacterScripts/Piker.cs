using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Piker : MonoBehaviour
{
    public int coinsAmount;
    public AudioClip gotCollectible;

    public TextMeshProUGUI coinsText;
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
        if (collision.gameObject.tag == "collectible")
        {
            coinsAmount ++;
            coinsText.text = coinsAmount.ToString();

            GetComponent<CharacterController>().PlayVFXSound(gotCollectible, 0.8f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Test level Santiago Backup", LoadSceneMode.Single);
        }
    }
}
