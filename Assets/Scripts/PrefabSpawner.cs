using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform player;
    public float spawnDistance = 10f;
    public float spawnInterval = 30f;
    private float nextSpawnTime = 30f;

    [Range(0, 100)] public int probability0;
    [Range(0, 100)] public int probability1;
    [Range(0, 100)] public int probability2;
    [Range(0, 100)] public int probability3;
    [Range(0, 100)] public int probability4;
    [Range(0, 100)] public int probability5;
    [Range(0, 100)] public int probability6;
    [Range(0, 100)] public int probability7;

    private int[] thresholds;
    Pause pause;
    private void Start()
    {
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        thresholds = new int[8];
        thresholds[0] = probability0;
        thresholds[1] = thresholds[0] + probability1;
        thresholds[2] = thresholds[1] + probability2;
        thresholds[3] = thresholds[2] + probability3;
        thresholds[4] = thresholds[3] + probability4;
        thresholds[5] = thresholds[4] + probability5;
        thresholds[6] = thresholds[5] + probability6;
        thresholds[7] = thresholds[6] + probability7;
    }
    void Update()
    {
        if (!pause.pause)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnPrefab();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }

    void SpawnPrefab()
    {
        if (prefabs.Length == 0) return;

        GameObject prefabToSpawn = prefabs[GetWeightedRandom()];
        Vector3 spawnPosition = new Vector3(0, 0, player.position.z + spawnDistance);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

    }
    int GetWeightedRandom()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < thresholds[0]) return 0;
        if (randomValue < thresholds[1]) return 1;
        if (randomValue < thresholds[2]) return 2;
        if (randomValue < thresholds[3]) return 3;
        if (randomValue < thresholds[4]) return 4;
        if (randomValue < thresholds[5]) return 5;
        if (randomValue < thresholds[6]) return 6;
        return 7;
    }
}