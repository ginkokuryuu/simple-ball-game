using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : SoldierAI
{
    Vector3 originPos = new Vector3();
    bool isChasingAttacker = false;
    bool isCollidingAttacker = false;
    bool isAttackerFound = false;
    AttackerAI targetAttacker;

    public Vector3 OriginPos { get => originPos; set => originPos = value; }
    public bool IsChasingAttacker { get => isChasingAttacker; set => isChasingAttacker = value; }
    public bool IsCollidingAttacker { get => isCollidingAttacker; set => isCollidingAttacker = value; }
    public bool IsAttackerFound { get => isAttackerFound; set => isAttackerFound = value; }
    public AttackerAI TargetAttacker { get => targetAttacker; set => targetAttacker = value; }

    protected override void Start()
    {
        base.Start();
        originPos = transform.position;
    }

    protected override void ConstructBehaviourTree()
    {
        IsGameOverNode isGameOverNode = new IsGameOverNode(this);

        IsInactiveNode isInactiveNode = new IsInactiveNode(this);
        HasReachedOriginNode hasReachedOriginNode = new HasReachedOriginNode(this);
        ReturnToOriginNode returnToOriginNode = new ReturnToOriginNode(this);

        IsChasingAttackerNode isChasingAttackerNode = new IsChasingAttackerNode(this);
        CaughtAttackerNode caughtAttackerNode = new CaughtAttackerNode(this);
        InactivateNode inactivateNode = new InactivateNode(this);

        IsAttackerFoundNode isAttackerFoundNode = new IsAttackerFoundNode(this);
        ChaseAttackerNode chaseAttackerNode = new ChaseAttackerNode(this);

        SearchAttackerNode searchAttackerNode = new SearchAttackerNode(this);

        SelectorNode tryReturnToOriginSel = new SelectorNode(new List<Node>
        {
            hasReachedOriginNode,
            returnToOriginNode
        });
        SequenceNode tryToReturnSeq = new SequenceNode(new List<Node>
        {
            isInactiveNode,
            tryReturnToOriginSel
        });

        SequenceNode collideWithAttackerSeq = new SequenceNode(new List<Node>
        {
            isChasingAttackerNode,
            caughtAttackerNode,
            inactivateNode
        });
        SequenceNode tryToChaseSeq = new SequenceNode(new List<Node>
        {
            isAttackerFoundNode,
            chaseAttackerNode
        });
        SelectorNode activeSel = new SelectorNode(new List<Node>
        {
            collideWithAttackerSeq,
            tryToChaseSeq,
            searchAttackerNode
        });

        rootBehaviourNode = new SelectorNode(new List<Node>
        {
            isGameOverNode,
            tryToReturnSeq,
            activeSel
        });
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        base.Activate();
        originPos.Set(OriginPos.x, transform.position.y, OriginPos.z);
    }

    public void ChangeMoveTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("AttackerSoldier"))
        {
            isCollidingAttacker = true;
            StartCoroutine(AttackerCollisionBuffer());
        }
    }

    IEnumerator AttackerCollisionBuffer()
    {
        yield return new WaitForEndOfFrame();
        isCollidingAttacker = false;
    }
}
