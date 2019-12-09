using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class DumbBlob : Blob
{
    [SerializeField] protected float radius = 5f;

    private void Awake()
    {
        isCollectable = true;
        _isCollected = false;
    }

    protected override void Think()
    {
        var rand = RandomPosInNavMeshSphere(transform.position,radius);
        rand.y = 0;
        //var dest = transform.position + rand;
        agent.destination = rand;
        source.clip = run;
        source.Play();
    }


    private void Update()
    {
        if (!_canThink) return;
        Vector3 pos1 = agent.destination;
        pos1.y = 0;
        Vector3 pos2 = agent.transform.position;
        pos2.y = 0;
        if (Vector3.Distance(pos1, pos2) < 0.5f)
        {
            Think();
        }
    }
    
}
