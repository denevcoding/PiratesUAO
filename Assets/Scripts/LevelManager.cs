using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState{
    Intro,
    Gameplay
}

public class LevelManager : MonoBehaviour
{
    public SpawnPoint lastCheckPoint;
    public CharacterController character;

    public AudioSource levelMusicSource;
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;


    public GameState gameState = GameState.Intro;
    

    // Start is called before the first frame update
    void Start()
    {

        levelMusicSource = GetComponent<AudioSource>();

        character.lvlManager = this;


        gameState = GameState.Intro;
        character.gameObject.SetActive(false);

        PlayMusic(menuMusic, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       
    public void CheckPoint()
    {
        
    }



    public void InitializeGame()
    {
        character.gameObject.SetActive(true);
        PlayMusic(gameplayMusic, 0.4f);
    }


    public void Respawn()
    {
        character.gameObject.SetActive(false);
        character.transform.position = lastCheckPoint.transform.position;
        character.Reborn();
        character.gameObject.SetActive(true);
    }


    public void RespawnPlatform(GameObject platform, Vector3 position, float time)
    {
        StartCoroutine(ReActivatePlatform(platform, time));        
    }


    IEnumerator ReActivatePlatform(GameObject platform, float time)
    {        
        yield return new WaitForSeconds(time);
        platform.SetActive(true);
       
    }




    public void PlayMusic(AudioClip clip, float volume)
    {
        levelMusicSource.clip = clip;
        levelMusicSource.volume = volume;
        levelMusicSource.Play();
    }

}


