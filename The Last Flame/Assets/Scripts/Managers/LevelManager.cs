using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using System;

public class LevelManager : Singleton<LevelManager> {

    public string currentScene;
    public float timerFase = 0;
    public int level;

    Player player;

    void Start()
    {
        this.player = FindObjectOfType<Player>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    void Update()
    {
        //timerFase += Time.deltaTime;
        //EstorouTempo();
    }

    public void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().name;

        if(currentScene == "Game")
        {
            SoundManager.instance.musicSource.clip = SoundManager.instance.bgm[1];
            SoundManager.instance.musicSource.volume = 0.2f;
            SoundManager.instance.musicSource.Play();

            SoundManager.instance.musicSource2.clip = SoundManager.instance.bgm[3];
            SoundManager.instance.musicSource2.volume = 1f;
            SoundManager.instance.musicSource2.Play();
        }
    }

    public void ProximaFase()
    {
        SceneManager.LoadScene("Level" + level);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    //void EstorouTempo()
    //{
    //    if (timerFase >= 300)
    //    {
    //        player.Die();
    //       // ProximaFase();
    //       // SceneManager.LoadScene("GameOver");
    //        Debug.Log("EntrouEstorouTempo");
 
    //    }
 
    //}

}
