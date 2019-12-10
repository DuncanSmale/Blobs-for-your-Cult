using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Crane : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private Vector2 bounds;

    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private GameObject cage;
    [SerializeField] private float timeToDrop = 2f;
    [SerializeField] private float timeToLift = 1f;
    [SerializeField] private float timeToMove = 3f;
    [SerializeField] private float timeToWaitAtBottom = 0.5f;
    [SerializeField] private GameObject placePosition;
    [SerializeField] private GameObject collectPosition;
    [SerializeField] private float rotationSpeed = 5f;

    private Vector3 _move = Vector3.zero;
    private Vector3 _cagePosStart = Vector3.zero;
    private Vector3 _cagePosEnd = Vector3.zero;

    [SerializeField] private float radius = 2f;
    [SerializeField] private GameObject checkPosition;
    [SerializeField] private LayerMask checkMask;

    private Collider[] _blobs;
    
    private bool _placed = false;
    private bool _collected = false;

    private Tween _dropTween;
    private Tween _liftTween;

    private void SetCage()
    {
        _cagePosStart = cage.transform.position;
        _cagePosEnd = _cagePosStart;
        _cagePosEnd.y = 0;
    }

    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && _placed == false)
        {
            _placed = true;
            SetCage();
            StartCoroutine(nameof(PlaceRoutine));
        }

        if (!_placed)
        {
            if (Math.Abs(_move.x) > 0.1f || Math.Abs(_move.z) > 0.1f)
                Move();
        }
    }

    private void Drop()
    {
        SetCage();
        _dropTween = transform.DOMove(_cagePosEnd, timeToDrop);
        _dropTween.onComplete += OnCompleteDrop;
    }

    private void OnCompleteDrop()
    {
        
    }

    private void Lift()
    {
        _liftTween = transform.DOMove(_cagePosStart, timeToLift);
        _liftTween.onComplete += OnCompleteLift;
    }

    private void OnCompleteLift()
    {
        
    }

    //coroutine to determine how the crane goes through its drop sequence
    private IEnumerator PlaceRoutine()
    {
        SoundController.instance.RandomPitchandsfx(clip);//sound effects played 
        
        Drop(); //drops the crane
        yield return new WaitForSeconds(timeToDrop); //waits till the crane is at the bottom

        var check = CheckCollect(); //checks if there are any slimes that can be picked up
        yield return new WaitForSeconds(timeToWaitAtBottom); //waits while at the bottom 

        Lift(); //lifts crane up
        yield return new WaitForSeconds(timeToLift);//waits till the crane is at the top

        if (!check)//if no blobs are collected, end routine
        {
            _placed = false;
            yield break;
        }

        transform.DOMove(placePosition.transform.position + Vector3.up * 8f, timeToMove);//moves the crane to the pen position
        yield return new WaitForSeconds(timeToMove);//waits till the crane is at the pen
        
        Drop(); //drops crane
        yield return new WaitForSeconds(timeToDrop);//waits till at the bottom
        
        foreach (var blob in _blobs)//drops each blob
        {
            blob.GetComponent<Blob>().Place();
        }
        
        yield return new WaitForSeconds(timeToWaitAtBottom);//waits at the bottom
        Lift();//lifts crane
        yield return new WaitForSeconds(timeToLift);//waits till crane is lifted
        
        _placed = false;
    }

    private bool CheckCollect()
    {
        _blobs = Physics.OverlapSphere(checkPosition.transform.position, radius, checkMask.value);
        List<GameObject> children = new List<GameObject>();
        int numCollectable = 0;
        foreach (var obj in _blobs)
        {
            Blob b = obj.GetComponent<Blob>();
            if (b._type == BlobType.Yellow)
            {
                foreach (var blob in _blobs)
                {
                    if (blob == obj) continue;
                    var collect = blob.GetComponent<Blob>().canCollect(_blobs.Length);
                    if (collect)
                        numCollectable++;
                }
            }
            var rand = Random.insideUnitSphere * 0.5f;
            var collectable = b.canCollect(numCollectable);
            if (!collectable) continue;
            children.Add(obj.gameObject);
            Debug.Log(obj.name);
            obj.GetComponent<Blob>().PickUp();
            rand.y = 0;
            obj.transform.position = collectPosition.transform.position + rand;
            obj.transform.SetParent(cage.transform);
        }

        return children.Count > 0;
    }

    private void Move()
    {
        _move.Normalize();
        transform.position = Vector3.Lerp(transform.position, transform.position + _move, Time.deltaTime * lerpSpeed);
        float xClamp = Mathf.Clamp(transform.position.x, -bounds.x, bounds.x);
        float zClamp = Mathf.Clamp(transform.position.z, -bounds.y, bounds.y);
        Vector3 newPos = transform.position;
        newPos.x = xClamp;
        newPos.z = zClamp;
        transform.position = newPos;

        if(_move != Vector3.zero)
            cage.transform.rotation = Quaternion.Slerp(cage.transform.rotation, Quaternion.LookRotation(_move.normalized), Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(checkPosition.transform.position, radius);
    }
}
