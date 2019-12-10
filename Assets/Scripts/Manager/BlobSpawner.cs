using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject[] Blobs;
    [SerializeField] protected int numBlobs = 20;
    [SerializeField] protected float minDelay = 2f;
    [SerializeField] protected float maxDelay = 4f;
    [SerializeField] protected float spawnRadius = 30f;

    private float timeNextSpawn = 0;
    
    void Start()
    {
        for (int i = 0; i < numBlobs; i++)
        {
            SpawnBlob();
        }
    }

    private void SpawnBlob()
    {
        int rand = Random.Range(0, Blobs.Length);
        Vector3 randPos = Random.insideUnitSphere * spawnRadius;
        randPos.y = 0;
        Instantiate(Blobs[rand], transform.position + randPos, Quaternion.identity);
    }
    
    private void Update()
    {
        if (Time.time > timeNextSpawn)
        {
            SpawnBlob();
            timeNextSpawn = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
