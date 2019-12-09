using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBlob : Blob
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private float waitTime = 2f;

    private float stopTime = 0;
    private bool stopped = false;
    
    protected override void Think()
    {
        var rand = RandomPosInNavMeshSphere(transform.position,radius);
        rand.y = 0;
        //var dest = transform.position + rand;
        agent.destination = rand;
        stopped = false;
        source.clip = run;
        source.Play();
    }

    void Start()
    {
        Think();
        isCollectable = true;
        _isCollected = false;
    }

    
    void Update()
    {
        if (!_canThink) return;
        Vector3 pos1 = agent.destination;
        pos1.y = 0;
        Vector3 pos2 = agent.transform.position;
        pos2.y = 0;
        if (Vector3.Distance(pos1,pos2) < 0.5f && !stopped)
        {
            stopped = true;
            stopTime = Time.time + Random.Range(waitTime / 2, waitTime);
            source.Stop();
        }
        if(Time.time > stopTime && stopped)
            Think();
    }
}
