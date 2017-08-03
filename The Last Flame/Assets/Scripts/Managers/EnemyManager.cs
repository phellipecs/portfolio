using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public enum INIMIGOS
{
    BabauNv1,
    DretchNv1,
    Boss
}

public class EnemyManager : MonoBehaviour {

    public GameObject[] enemys;                // The enemy prefab to be spawned.
    private int indiceEnemy = 0;
    private INIMIGOS inimigo;
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int NumberEnemy = 0;
    public int MaxEnemy;
    public float cdSpawn = 5f;
    float timerSpawn;
    public float countTempo;
    public int waveAtual = 1;
    public bool finishSpawn = false;

	void Start ()
    {
        StartCoroutine(SpawnCoroutine());
	}
	
	void Update ()
    {
        if (!finishSpawn)
        {
            countTempo += Time.deltaTime;

            if (countTempo >= 30f)
                waveAtual = 2;

            if (countTempo >= 60f)
                waveAtual = 3;

            if (countTempo >= 90f)
                waveAtual = 4;

            if (countTempo >= 120f)
                waveAtual = 5;

            //if (timerSpawn >= 60f)
            //{
            //    MaxEnemy += 1;
            //    timerSpawn = 0;
            //}

            //if (cdSpawn >= 5)
            //{
            //    spawnTime -= 0.2f;
            //    cdSpawn = 0;

            //    if (spawnTime <= 1f)
            //    {
            //        spawnTime = 1f;
            //    }
            //}
        }
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (!finishSpawn)
            {
                Spawn();
            }
            else
            {
                StopCoroutine(SpawnCoroutine());
            }
                

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        if (NumberEnemy < MaxEnemy)
        {
            float sort = Random.value;

            
            #region Wave 1
            if(waveAtual == 1)
            {
                // 0,2 - 0,8
                if (sort > 0.1 && sort < 0.9)
                {
                    // Dretch nv1
                    indiceEnemy = (int)INIMIGOS.DretchNv1;
                }
                else if (sort <= 0.1)
                {
                    indiceEnemy = (int)INIMIGOS.BabauNv1;
                }
            }
            #endregion

            #region Wave 2
            if (waveAtual == 2)
            {
                // 0,4 - 0,7
                if (sort > 0.3 && sort < 0.7)
                {
                    indiceEnemy = (int)INIMIGOS.DretchNv1;
                }
                else if (sort < 0.3)
                {
                    indiceEnemy = (int)INIMIGOS.BabauNv1;
                }       
            }
            #endregion

            #region Wave 3
            if (waveAtual == 3)
            {
                // 0 - 0,33
                if(sort < 0.34f)
                {
                    indiceEnemy = (int)INIMIGOS.DretchNv1;
                }
                else if (sort > 0.34f && sort < 0.66f)
                {
                    indiceEnemy = (int)INIMIGOS.DretchNv1;
                }
                else if (sort > 0.66f && sort < 1f)
                {
                    indiceEnemy = (int)INIMIGOS.BabauNv1;
                }       
            }
            #endregion

            #region Wave 4
            if (waveAtual == 4)
            {
                // 0 - 0,59
                if (sort > 0f && sort < 0.6f)
                {
                    indiceEnemy = (int)INIMIGOS.DretchNv1;
                }
                else if (sort > 0.6f && sort <= 0.79f)
                {
                    indiceEnemy = (int)INIMIGOS.BabauNv1;
                }
                else if (sort >= 0.8f && sort <= 1f)
                {
                    indiceEnemy = (int)INIMIGOS.BabauNv1;
                }   
            }
            #endregion

            #region Wave 5
            if (waveAtual == 5)
            {
                if(countTempo >= 180)
                {
                    indiceEnemy = (int)INIMIGOS.Boss;
                    finishSpawn = true;
                }
				if(NumberEnemy==0)
				{
                indiceEnemy = (int)INIMIGOS.Boss;
                finishSpawn = true;
				
				}
            }
            #endregion

            if (Instantiate(enemys[indiceEnemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation))
            {
                Debug.Log("Wave: " + waveAtual + " / Inimigo: " + enemys[indiceEnemy].gameObject.name);
                NumberEnemy++;
            }
        }
    }
}
