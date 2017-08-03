using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerFase2 : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int NumberEnemy = 0;
    public int MaxEnemy;

    float timer;

	void Start () {

        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
	
	void Update () {

        timer += Time.deltaTime;

        if (timer >= 45)
        {
            MaxEnemy += 1;
            timer = 0;
        }

	}

    public void Spawn()
    {
        //If the player has no health left...
        /* if (PlayerHealth1.vidaAtual <= 0f)
         {
             return;
         }*/

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        if (NumberEnemy < MaxEnemy)
        {
            if (Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation))
            {
                NumberEnemy++;
            }
        }

    }
}
