using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject [] animalPrefabs;
    public int animalIndex;

    public float spawnRange = 18f; //spawn range along walls
    private float wallDistance = 28f; //wall placed at 30f - 2f for width

    public Transform player;
    public float safeRadius = 15f;

    private float startDelay = 2f;
    private float spawnIntreval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnIntreval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);

        Vector3 spawnPos;
        bool validPosition = false;
        int attempts = 0;

        do
        {
            int side = Random.Range(0, 4);

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

                default:
                    spawnPos = Vector3.zero;
                    break;
            }

            float distanceToPlayer = Vector3.Distance(spawnPos, player.position);
            validPosition = distanceToPlayer > safeRadius;

            attempts++;

        } while (!validPosition && attempts < 10);

        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.identity);
    }
}
