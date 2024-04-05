using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the array and wave logic for the zombie spawners
public class Spawner : MonoBehaviour
{
    [Header ("Wave Varibles")]
    private int waveNumber = 0;
    public int spawnAmount = 0;
    public int zombiesKilled = 0;

    [Header("Spawner Varibles")]
    public GameObject[] spawners;
    public GameObject zombie;

    //set the spawners arary to 5 and set all points that are a child of Spawner
    //start wave
    private void Start()
    {
        spawners = new GameObject[5];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();
    }

    //if the zombies killed are more than or equal to the amount spawned, start the next wave
    private void Update()
    {
        if (zombiesKilled >= spawnAmount)
        {
            NextWave();
        }
    }

    //Spawn the enemy from a randomly selected spawner from the array
    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(zombie, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
    }

    //The first wave, if the number of zombies is less than the spawn ammoutn spawn one more
    public void StartWave()
    {
        waveNumber = 1;
        spawnAmount = 2;
        zombiesKilled = 0;

        for(int i = 0; i < spawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    //update the wave number and spawn an extra 2 zombies, set number of zombies killed back to 0
    public void NextWave()
    {
        waveNumber++;
        spawnAmount += 2;
        zombiesKilled = 0;

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnEnemy();
        }
    }
}
