using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDelete : MonoBehaviour
{
    private GameObject player;
    public float x;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }
    void Update()
    {
        if (this.gameObject.transform.position.z + 80 < player.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
