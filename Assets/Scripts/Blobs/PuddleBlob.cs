using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuddleBlob : DumbBlob
{

    [SerializeField] protected float minPuddleTime = 1f;
    [SerializeField] protected float maxPuddleTime = 1.5f;
    [HideInInspector] protected bool _isPuddle = false;
    [SerializeField] protected Vector3 endScale;
    [SerializeField] protected GameObject bodyEffect;
    
    private Vector3 _scale;
    private int _iterations = 50;

    private float endPuddle = 0;

    private void Awake()
    {
        _scale = transform.localScale;
        isCollectable = true;
        _isCollected = false;
    }

    protected IEnumerator MakePuddle()
    {
        isCollectable = false;
        Instantiate(bodyEffect, new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z), Quaternion.Euler(0,0,0), transform);

        for (int i = 0; i < _iterations; i++)
        {
            transform.localScale = Vector3.Lerp(_scale, endScale, t: (1f * i) / _iterations);
            yield return new WaitForSeconds(0.002f);
        }
        transform.localScale = endScale;
    }

    protected IEnumerator UnMakePuddle()
    {
        for (int i = 0; i < _iterations; i++)
        {
            transform.localScale = Vector3.Lerp(endScale, _scale, (1f * i) / _iterations);
            yield return new WaitForSeconds(0.002f);
        }
        transform.localScale = _scale;
        isCollectable = true;
    }

    protected void Puddle()
    {
        _isPuddle = true;
        endPuddle = Time.time + Random.Range(minPuddleTime, maxPuddleTime);
        StartCoroutine(nameof(MakePuddle));
    }

    protected void EndPuddle()
    {
        _isPuddle = false;
        StartCoroutine(nameof(UnMakePuddle));
    }

    private void Update()
    {
        if (!_canThink) return;
        var pos1 = agent.destination;
        pos1.y = 0;
        var pos2 = agent.transform.position;
        pos2.y = 0;
        if (Vector3.Distance(pos1, pos2) < 0.5f && !_isPuddle)
        {
            Puddle();
            source.Stop();
            agent.enabled = false;
        }
        if (_isPuddle && Time.time > endPuddle)
        {
            agent.enabled = true;
            EndPuddle();
            source.clip = run;
            source.Play();
            Think();
        }
    }
}
