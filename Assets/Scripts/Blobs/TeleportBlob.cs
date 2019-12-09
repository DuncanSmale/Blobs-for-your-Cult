using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlob : PuddleBlob
{

    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 2f;

    private float timeNextTeleport = 0;
    private bool _canTeleport = true;

    void Update()
    {
        if (Time.time > timeNextTeleport && _canTeleport)
        {
            StartCoroutine(nameof(Teleport));
        }
        else
        {
            source.Stop();
        }
    }

    private IEnumerator Teleport()
    {
        _canTeleport = false;
        Vector3 teleportPos = Random.insideUnitSphere * radius;
        Puddle();
        Instantiate(bodyEffect, transform.position, Quaternion.Euler(-90,0,0), transform);
        yield return new WaitForSeconds(1.5f);
        transform.position += teleportPos;
        source.clip = run;
        source.Play();
        EndPuddle();
        timeNextTeleport = Time.time + Random.Range(minWaitTime, maxWaitTime);
        _canTeleport = true;
    }
}
