using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballSpeed = 1.5f;
    [SerializeField] private float distanceTolerance = 0.1f;
    private Transform target;
    private bool isMoving = false;
    private bool isBeingHeld = false;
    Transform originParent;
    Rigidbody rb;

    LayerMask originLayer;

    public bool IsBeingHeld { get => isBeingHeld; set => isBeingHeld = value; }

    private void Awake()
    {
        ObjectLoader.INSTANCE.Ball = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originParent = transform.parent;
        rb = GetComponent<Rigidbody>();
        originLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = (target.position - transform.position);
            float distance = direction.sqrMagnitude;
            if(distance <= distanceTolerance)
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }

            rb.velocity = direction.normalized * ballSpeed;
        }
    }

    public void HoldBy(Transform spot)
    {
        rb.isKinematic = true;
        transform.parent = spot;
        transform.localPosition = Vector3.zero;
        isBeingHeld = true;
        gameObject.layer = 13;
    }

    public void PassToAlly(AttackerAI closestAlly)
    {
        rb.isKinematic = false;
        if (rb.IsSleeping())
            rb.WakeUp();

        gameObject.layer = originLayer;
        isBeingHeld = false;
        transform.parent = originParent;
        target = closestAlly.transform;
        isMoving = true;
    }
}
