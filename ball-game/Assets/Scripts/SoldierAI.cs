using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Owner { Player1, Player2 }

public class SoldierAI : MonoBehaviour
{
    [SerializeField] protected float reactivateTime = 2.5f;
    protected Node rootBehaviourNode;
    protected bool isActive = false;
    protected bool isMoving = false;
    protected float moveSpeed = 0f;
    protected Collider _collider;
    [SerializeField] protected Owner player = Owner.Player1;
    protected Rigidbody rb;
    Vector3 lookVector = new Vector3();

    float inactiveTime = 0.5f;
    float counterTime = 0f;

    protected Transform target;

    protected LayerMask originLayer;

    public Owner Player { get => player; set => player = value; }

    protected virtual void Awake()
    {
        MyEventSystem.INSTANCE.OnRoundEnd += OnRoundEnd;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        originLayer = gameObject.layer;

        ConstructBehaviourTree();
    }

    protected virtual void Update()
    {
        rootBehaviourNode.Evaluate();

        if (!isActive)
        {
            counterTime += Time.deltaTime;
            if(counterTime >= inactiveTime)
            {
                Activate();
            }
        }

        if (isMoving)
        {
            if (target)
            {
                lookVector.Set(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(lookVector, transform.up);
            }

            rb.velocity = transform.forward * moveSpeed;
        }
    }

    protected virtual void ConstructBehaviourTree()
    {
        return;
    }

    public void Move(float speed)
    {
        moveSpeed = speed;
        isMoving = true;
    }

    public void StopMove()
    {
        isMoving = false;
    }

    public virtual void Activate()
    {
        isActive = true;
        counterTime = 0f;
        inactiveTime = reactivateTime;
        gameObject.layer = originLayer;
    }

    public void Inactivate()
    {
        gameObject.layer = 10;
        isActive = false;
        isMoving = false;
    }

    public bool StillInactive()
    {
        return !isActive;
    }

    public virtual void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void OnRoundEnd()
    {
        DestroySelf();
    }

    private void OnDestroy()
    {
        MyEventSystem.INSTANCE.OnRoundEnd -= OnRoundEnd;
    }
}
