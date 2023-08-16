using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    //public GameObject snowflakePrefab;
    public GameObject CirclePrefab;

    void Start() {
        //InvokeRepeating("SpawnSnowflake", 0f, 0.5f);
        InvokeRepeating("Circle", 0f, 0.5f);
    }

    // void SpawnSnowflake()
    // {
    //     float xPos = Random.Range(-9f, 9f);
    //     Vector3 spawnPos = new Vector3(xPos, 7f, 0f);

    //     GameObject snowflake = Instantiate(snowflakePrefab, spawnPos, Quaternion.identity);
    //     snowflake.transform.parent = transform;
    // }

    void Spawn() {
        float xPos = Random.Range(-9f, 9f);
        Vector3 spawnPos = new Vector3(xPos, 7f, 0f);

        GameObject snowflake = Instantiate(CirclePrefab, spawnPos, Quaternion.identity);
        snowflake.transform.parent = transform;
    }
}
