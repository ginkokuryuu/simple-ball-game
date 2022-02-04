using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float maxTimer = 140f;
    [SerializeField] TMPro.TMP_Text timerText = null;
    float counter = 0f;
    bool isMatchRunning = false;

    private void Awake()
    {
        MyEventSystem.INSTANCE.OnRoundStart += OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd += OnRoundEnd;
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
        GameManager.INSTANCE.RoundClear(Owner.TimeOut);
    }

    public void OnRoundStart()
    {
        isMatchRunning = true;
        counter = maxTimer;
    }

    public void OnRoundEnd()
    {
        isMatchRunning = false;
    }

    private void OnDestroy()
    {
        MyEventSystem.INSTANCE.OnRoundStart -= OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd -= OnRoundEnd;
    }
}
