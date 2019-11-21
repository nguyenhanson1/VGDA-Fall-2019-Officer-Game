using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPoints; //the coordinates that serve as the center for spawn points
    [SerializeField] private float spawnSpread; //the maximum distance from a spawn point a character can spawn
    private float timeSinceSpawn = 0; //the amount of time that has passed since the last spawn
    [SerializeField] private float spawnTime; //the time between spawns
    [SerializeField] private ObjectPooler objectPool; // ObjectPool of the enemy
    [SerializeField] private int numPerSpawn; //how many objects to spawn every wave
    [SerializeField] private float difficulty = 1; //used to modify number of spawns as wave number increases
    [SerializeField] private float spawnDelay = 0.5f; //the delay between spawning groups when spawning more objects than spawn points

    private int waveNumber = 0; //the number of waves that have been spawned
    private bool isSpawning = false; //whether the manager is currently spawning

    private void Start()
    {
        StartCoroutine("SpawnWave");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnTime && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine("SpawnWave");
        }
    }

    public int getWaveNumber()
    {
        return waveNumber;
    }

    //spawns a wave of objects
    IEnumerator SpawnWave()
    {
        int numToSpawn = (int) (difficulty * numPerSpawn);
        List<Vector3> spawnList = new List<Vector3>();
        spawnList.AddRange(spawnPoints);

        if (numToSpawn <= spawnPoints.Length) //check if we can spawn this wave all at once
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                int randPos = Random.Range(0, spawnList.Count); //get a random spawn point
                Spawn(spawnList[randPos]); //spawn at that spawn point
                spawnList.Remove(spawnList[randPos]); //remove so we only spawn at it once
            }
        }
        else
        {
            while (numToSpawn > 0) //keep iterating through spawn points until we're done spawning
            {
                for (int i = 0; i < numToSpawn; i++)
                {
                    int randPos = Random.Range(0, spawnList.Count); //get a random spawn point
                    Spawn(spawnList[randPos]); //spawn at that spawn point
                    spawnList.Remove(spawnList[randPos]); //remove so we only spawn at it once
                    numToSpawn--;
                }
                spawnList = new List<Vector3>();
                spawnList.AddRange(spawnPoints); //refill our list of spawn points
                //Debug.Log("Spawned, " + numToSpawn + " Remain");
                yield return new WaitForSeconds(1f); //wait before continuing to spawn
            }
        }

        //Debug.Log("Spawned");
        timeSinceSpawn = 0;
        waveNumber++;
        difficulty += 0.5f;
        isSpawning = false;
    }

    void Spawn(Vector3 spawnPos)
    {
        int randx = Random.Range(0, 2);
        int randy = Random.Range(0, 2);
        int randz = Random.Range(0, 2);

        if (randx == 0)
        {
            randx--;
        }
        if (randy == 0)
        {
            randy--;
        }
        if (randz == 0)
        {
            randz--;
        }

        GameObject enemy = objectPool.GetGenericBullet();
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(spawnPos.x + Random.Range(0, spawnSpread) * randx,
                                                spawnPos.y + Random.Range(0, spawnSpread) * randy,
                                                spawnPos.z + Random.Range(0, spawnSpread) * randz);
    }

    private void OnDrawGizmos() //draws waypoints and spawn area
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.DrawCube(spawnPoints[i], new Vector3(spawnSpread * 2, spawnSpread * 2, spawnSpread * 2));
        }
    }
}
