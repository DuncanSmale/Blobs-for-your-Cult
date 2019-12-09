using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum BlobType
{
    Purple,
    Red,
    Blue,
    Green,
    Yellow
}

public abstract class Blob : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] public BlobType _type;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected AudioClip run;

    protected bool _canThink = true;
    [HideInInspector] public bool _isCollected = false;
    [HideInInspector] public bool isCollectable = true;

    protected abstract void Think();

    private void Start()
    {
        Think();
    }

    protected Vector3 RandomPosInNavMeshSphere(Vector3 origin, float distance)
    {
        var randDir = Random.insideUnitSphere * distance;
        randDir += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, distance, NavMesh.AllAreas);
        return navHit.position;
    }
    
    public virtual bool canCollect(int numBlobs)
    {
        return !_isCollected && isCollectable;
    }

    public virtual void PickUp()
    {
        _isCollected = true;
        _canThink = false;
        agent.isStopped = true;
        agent.enabled = false;
    }

    public virtual void Place()
    {
        _canThink = true;
        agent.enabled = true;
        agent.transform.SetParent(null);
        agent.isStopped = false;
        BlobUIManager.Instance.IncreaseBlobUI(_type);
    }
}
