using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject [] animalPrefabs;
    public int animalIndex;

    public float spawnrangex = 18f;
    private float spwanPosZ = 20f;

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
        Vector3 spawnpos = new Vector3(Random.Range(-spawnrangex, spawnrangex), 0, spwanPosZ);
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], spawnpos, animalPrefabs[animalIndex].transform.rotation);
    }
}
