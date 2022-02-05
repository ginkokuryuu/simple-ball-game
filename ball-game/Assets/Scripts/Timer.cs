using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float roundMaxTimer = 140f;
    [SerializeField] TMPro.TMP_Text timerText = null;
    float counter = 0f;
    bool isMatchRunning = false;
    bool isMaze = false;

    private void Awake()
    {
        MyEventSystem.INSTANCE.OnRoundStart += OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd += OnRoundEnd;
        MyEventSystem.INSTANCE.OnMazeStart += OnMazeStart;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isMatchRunning)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                TimeOut();
            }
        }
        timerText.text = counter.ToString("0");
    }

    void TimeOut()
    {
        isMatchRunning = false;
        if (isMaze)
            MazeHandler.INSTANCE.OnTimerRunOut();
        else
            GameManager.INSTANCE.RoundClear(Owner.TimeOut);
    }

    public void OnMazeStart()
    {
        isMatchRunning = true;
        isMaze = true;
        counter = roundMaxTimer;
    }

    public void OnRoundStart()
    {
        isMatchRunning = true;
        isMaze = false;
        counter = roundMaxTimer;
    }

    public void OnRoundEnd()
    {
        isMatchRunning = false;
    }

    private void OnDestroy()
    {
        MyEventSystem.INSTANCE.OnRoundStart -= OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd -= OnRoundEnd;
        MyEventSystem.INSTANCE.OnMazeStart -= OnMazeStart;
    }
}
