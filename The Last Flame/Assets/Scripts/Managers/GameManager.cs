using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [System.Serializable]
    public class GameSettings
    {
        public bool isFullScreen;
    }

    [System.Serializable]
    public class GameRules
    {
        public bool inGame;
        public bool gameOver;
        public bool paused;
    }

    public GameRules gameRules;
    public GameSettings gameSettings;
    public GameObject respawnEnemys;
    [HideInInspector]
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (gameRules.paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SpawnEnemys()
    {
        Instantiate(respawnEnemys, Vector3.zero, transform.rotation);
    }

    public void TogglePause()
    {
        gameRules.paused = !gameRules.paused;
    }

    public void ToggleFullScreen()
    {
        gameSettings.isFullScreen = !gameSettings.isFullScreen;
        Screen.fullScreen = gameSettings.isFullScreen;
    }

}
