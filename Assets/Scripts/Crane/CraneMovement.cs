using System;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float rotationSpeed = 5f;
    #endregion
    
    public bool CanMove { get; set; } = true;
    [HideInInspector] public GameObject cage;
    
    private Vector3 _move = Vector3.zero;
    
    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.z = Input.GetAxisRaw("Vertical");

        if (!CanMove) return;
        if (Math.Abs(_move.x) > 0.1f || Math.Abs(_move.z) > 0.1f)
            Move();
    }
    
    private void Move()
    {
        _move.Normalize();
        transform.position = Vector3.Lerp(transform.position, transform.position + _move, Time.deltaTime * lerpSpeed);
        /*float xClamp = Mathf.Clamp(transform.position.x, -bounds.x, bounds.x);
        float zClamp = Mathf.Clamp(transform.position.z, -bounds.y, bounds.y);
        Vector3 newPos = transform.position;
        newPos.x = xClamp;
        newPos.z = zClamp;
        transform.position = newPos;*/

        if(_move != Vector3.zero)
            cage.transform.rotation = Quaternion.Slerp(cage.transform.rotation, Quaternion.LookRotation(_move.normalized), Time.deltaTime * rotationSpeed);
    }
}
