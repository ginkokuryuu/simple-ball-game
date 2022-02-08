using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeHandler : MonoBehaviour
{
    public static MazeHandler INSTANCE;

    [SerializeField] NavMeshSurface surface = null;

    [SerializeField] GameObject playerPrefab = null;
    [SerializeField] GameObject ballPrefab = null;
    [SerializeField] GameObject mazeGoal = null;

    [SerializeField] Transform ballContainer = null;
    [SerializeField] Transform agentContainer = null;
    NavMeshAgent playerAgent;
    GameObject ball;

    [SerializeField] MazeGenerator mazeGenerator = null;

    bool isRunning = false;
    bool isChasingBall = false;
    bool isChasingGoal = false;

    Vector3 destination = new Vector3();

    private void Awake()
    {
        INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isRunning)
            StartMaze();

        if (isRunning)
        {
            float distance = Vector3.Distance(playerAgent.transform.position, destination);

            if (distance <= 1f)
            {
                if (isChasingBall)
                    BallCaptured();
                else if (isChasingGoal)
                    GoalReached();
            }
        }
    }

    void RandomizeBallPos()
    {
        int width = Random.Range(1, 7);
        int height = Random.Range(1, 14);
        print(new Vector3(2.5f + (width * 5f), 0.7f, 5f + (height * 5f)));
        ball.transform.localPosition = new Vector3(2.5f + (width * 5f), 0.7f, 5f + (height * 5f));
    }

    public void StartMaze()
    {
        surface.BuildNavMesh();

        mazeGenerator.StartGeneratingMaze();

        ball = Instantiate(ballPrefab, ballContainer, false);
        RandomizeBallPos();

        GameObject player = Instantiate(playerPrefab, agentContainer, false);
        player.transform.localPosition = new Vector3(0, 1.5f, -11f);
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.speed = 1f * Const.scaleMultiplier;

        StartChase();
    }

    void StartChase()
    {
        isRunning = true;
        isChasingBall = true;
        destination.Set(ball.transform.position.x, playerAgent.transform.position.y, ball.transform.position.z);
        playerAgent.SetDestination(destination);
        print(ball.transform.position);
    }

    void BallCaptured()
    {
        isChasingBall = false;
        isChasingGoal = true;

        ball.transform.parent = playerAgent.transform.GetChild(0);
        ball.transform.localPosition = Vector3.zero;

        destination.Set(mazeGoal.transform.position.x, playerAgent.transform.position.y, mazeGoal.transform.position.z);
        playerAgent.SetDestination(destination);
    }

    void GoalReached()
    {
        ResetObject();
        GameManager.INSTANCE.MazeClear(true);
    }

    void ResetObject()
    {
        isChasingGoal = false;
        isRunning = false;
        isChasingBall = false;
        playerAgent.isStopped = true;

        Destroy(playerAgent.gameObject);
        Destroy(ball.gameObject);

        mazeGenerator.ResetMaze();
    }

    public void OnTimerRunOut()
    {
        ResetObject();
        GameManager.INSTANCE.MazeClear(false);
    }
}
