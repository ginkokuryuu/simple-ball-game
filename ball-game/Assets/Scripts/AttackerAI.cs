using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class AttackerAI : SoldierAI
{
    [SerializeField] private Transform ballHoldingSpot = null;
    bool isHoldingBall = false;
    bool isCollidingWithDefender = false;
    bool isCollidingWithBall = false;
    bool isCollidingWithGoal = false;
    bool isCollidingWithWall = false;
    AttackerAI closestAlly = null;
    Ball ball;

    public bool IsHoldingBall { get => isHoldingBall; }
    public bool IsCollidingWithDefender { get => isCollidingWithDefender; }
    public AttackerAI ClosestAlly { get => closestAlly; set => closestAlly = value; }
    public bool IsCollidingWithBall { get => isCollidingWithBall; set => isCollidingWithBall = value; }
    public bool IsCollidingWithGoal { get => isCollidingWithGoal; set => isCollidingWithGoal = value; }
    public bool IsCollidingWithWall { get => isCollidingWithWall; set => isCollidingWithWall = value; }

    private void Awake()
    {
        ObjectLoader.INSTANCE.AllAttackers.Add(this);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ball = ObjectLoader.INSTANCE.Ball;
    }

    protected override void ConstructBehaviourTree()
    {
        FindNearbyAllyNode findNearbyAllyNode = new FindNearbyAllyNode(this);
        PassToAllyNode passToAllyNode = new PassToAllyNode(this);
        InactivateNode inactivateNode = new InactivateNode(this);
        AttackerLoseNode attackerLoseNode = new AttackerLoseNode();
        IsHoldingBallNode isHoldingBallNode = new IsHoldingBallNode(this);
        TouchedByDefenderNode touchedByDefenderNode = new TouchedByDefenderNode(this);

        IsTouchingBallNode isTouchingBallNode = new IsTouchingBallNode(this);
        GrabBallNode grabBallNode = new GrabBallNode(this);
        FindBallNode findBallNode = new FindBallNode(this);
        ChaseBallNode chaseBallNode = new ChaseBallNode(this);

        IsTouchingGoalNode isTouchingGoalNode = new IsTouchingGoalNode(this);
        AttackerWinNode attackerWinNode = new AttackerWinNode();
        ChaseGoalNode chaseGoalNode = new ChaseGoalNode(this);

        IsTouchingWallNode isTouchingWallNode = new IsTouchingWallNode(this);
        DestroyNode destroyNode = new DestroyNode(this);
        GoForwardNode goForwardNode = new GoForwardNode(this);

        IsInactiveNode isInactiveNode = new IsInactiveNode(this);

        SequenceNode passBallSeq = new SequenceNode(new List<Node> 
        {
            findNearbyAllyNode,
            passToAllyNode,
            inactivateNode
        });
        SelectorNode tryToPassBallSel = new SelectorNode(new List<Node>
        {
            passBallSeq,
            attackerLoseNode
        });
        SequenceNode collideWithDefenderSeq = new SequenceNode(new List<Node>
        {
            isHoldingBallNode,
            touchedByDefenderNode,
            tryToPassBallSel
        });

        SequenceNode canGrabBallSeq = new SequenceNode(new List<Node>
        {
            isTouchingBallNode,
            grabBallNode
        });
        SelectorNode tryToGrabSel = new SelectorNode(new List<Node>
        {
            canGrabBallSeq,
            chaseBallNode
        });
        SequenceNode tryToChaseBallSeq = new SequenceNode(new List<Node>
        {
            findBallNode,
            tryToGrabSel
        });

        SequenceNode touchGoalSeq = new SequenceNode(new List<Node>
        {
            isTouchingGoalNode,
            attackerWinNode
        });
        SelectorNode tryToGoalSel = new SelectorNode(new List<Node>
        {
            touchGoalSeq,
            chaseGoalNode
        });
        SequenceNode bringBallToGoalSeq = new SequenceNode(new List<Node>
        {
            isHoldingBallNode,
            tryToGoalSel
        });

        SelectorNode touchEdgeSel = new SelectorNode(new List<Node>
        {
            isTouchingWallNode,
            isTouchingGoalNode
        });
        SequenceNode touchingWallSeq = new SequenceNode(new List<Node>
        {
            touchEdgeSel,
            destroyNode
        });
        SelectorNode goStraightSel = new SelectorNode(new List<Node>
        {
            touchingWallSeq,
            goForwardNode
        });

        SelectorNode activeSelector = new SelectorNode(new List<Node>
        {
            collideWithDefenderSeq,
            tryToChaseBallSeq,
            bringBallToGoalSeq,
            goStraightSel
        });

        rootBehaviourNode = new SelectorNode(new List<Node>
        {
            isInactiveNode,
            activeSelector
        });
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnMouseDown()
    {
        isCollidingWithDefender = true;
        StartCoroutine(DefenderCollisionBuffer());
    }

    public bool IsCollidingWall()
    {
        throw new NotImplementedException();
    }

    public void GrabBall()
    {
        isHoldingBall = true;
        gameObject.layer = 12;
        ball.HoldBy(ballHoldingSpot);

        ObjectLoader.INSTANCE.AttackerHoldingBall = this;
    }

    public void ChangeMoveTarget(Transform target)
    {
        this.target = target;
    }

    public void Move(float speed)
    {
        moveSpeed = speed;
        isMoving = true;
    }

    public void GoStraight(float speed)
    {
        this.target = null;
        Vector3 forward = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
        transform.LookAt(forward, transform.up);
        Move(speed);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void PassTheBall()
    {
        isHoldingBall = false;
        gameObject.layer = originLayer;
        ball.PassToAlly(closestAlly);

        if(ObjectLoader.INSTANCE.AttackerHoldingBall == this)
            ObjectLoader.INSTANCE.AttackerHoldingBall = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DefenderSoldier"))
        {
            isCollidingWithDefender = true;
            StartCoroutine(DefenderCollisionBuffer());
        }
        if (collision.collider.CompareTag("Ball"))
        {
            isCollidingWithBall = true;
            StartCoroutine(BallCollisionBuffer());
        }
        if (collision.collider.CompareTag("Goal"))
        {
            isCollidingWithGoal = true;
            StartCoroutine(GoalCollisionBuffer());
        }
        if (collision.collider.CompareTag("Wall"))
        {
            isCollidingWithWall = true;
            StartCoroutine(WallCollisionBuffer());
        }
    }

    IEnumerator DefenderCollisionBuffer()
    {
        yield return new WaitForEndOfFrame();
        isCollidingWithDefender = false;
    }

    IEnumerator BallCollisionBuffer()
    {
        yield return new WaitForEndOfFrame();
        isCollidingWithBall = false;
    }

    IEnumerator GoalCollisionBuffer()
    {
        yield return new WaitForEndOfFrame();
        isCollidingWithGoal = false;
    }

    IEnumerator WallCollisionBuffer()
    {
        yield return new WaitForEndOfFrame();
        isCollidingWithWall = false;
    }
}
