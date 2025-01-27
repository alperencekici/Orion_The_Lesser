using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IslandSpwaner : MonoBehaviour
{
    public GameObject[] islandPrefabs;
    public GameObject[] islandprefabsright;
    Pause pause;
    public Transform player;
    public Vector3 startPosition;
    public float spacing = 0f;
    public float spawnTriggerDistance = 700f;
    private float currentZ, currentZright;

    void Start()
    {
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        currentZ = startPosition.z;
        SpawnIsland();
    }

    void Update()
    {
        if (player.position.z + spawnTriggerDistance > currentZ && !pause.pause)
        {
            SpawnIsland();
        }
        if (player.position.z + spawnTriggerDistance > currentZright && !pause.pause)
        {
            SpawnIslandRight();
        }
    }

    void SpawnIsland()
    {
        GameObject islandPrefab = islandPrefabs[Random.Range(0, islandPrefabs.Length)];

        GameObject newIsland = Instantiate(islandPrefab);

        float islandLength = CalculatePrefabLength(newIsland);

        newIsland.transform.position = new Vector3(0, 0, currentZ);

        currentZ += islandLength + spacing;
    }
    void SpawnIslandRight()
    {
        GameObject islandPrefabright = islandprefabsright[Random.Range(0, islandprefabsright.Length)];

        GameObject newIslandright = Instantiate(islandPrefabright);

        float islandLengthright = CalculatePrefabLength(newIslandright);

        newIslandright.transform.position = new Vector3(0, 0, currentZright);

        currentZright += islandLengthright + spacing;
    }

    float CalculatePrefabLength(GameObject prefab)
    {
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0) return 0f;

        Bounds totalBounds = renderers[0].bounds;

        foreach (Renderer renderer in renderers)
        {
            totalBounds.Encapsulate(renderer.bounds);
        }

        return totalBounds.size.z;
    }
}
