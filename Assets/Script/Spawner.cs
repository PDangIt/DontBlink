using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject [] animalPrefabs;
    public int animalIndex;

    public float spawnRange = 18f; //spawn range along walls
    private float wallDistance = 28f; //wall placed at 30f - 2f for width

    private float startDelay = 2f;
    private float spawnIntreval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnIntreval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        /*Vector3 spawnpos = new Vector3(Random.Range(-spawnrangex, spawnrangex), 0, spwanPosZ);
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnpos, animalPrefabs[animalIndex].transform.rotation);*/
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        int side = Random.Range(0,4); //random for 4 walls
        Vector3 spawnPos = Vector3.zero;
        spawnPos = new Vector3(spawnPos.x, 0f, spawnPos.z);

        switch (side)
        {
            case 0: // North (+Z)
                spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, wallDistance);
                break;

            case 1: // South (-Z)
                spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, -wallDistance);
                break;

            case 2: // East (+X)
                spawnPos = new Vector3(wallDistance, 0, Random.Range(-spawnRange, spawnRange));
                break;

            case 3: // West (-X)
                spawnPos = new Vector3(-wallDistance, 0, Random.Range(-spawnRange, spawnRange));
                break;
        }

        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.identity);
    }
}
