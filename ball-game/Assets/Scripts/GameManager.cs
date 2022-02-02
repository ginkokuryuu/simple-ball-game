using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    
    [SerializeField] int currentMatch = 0;

    public int CurrentMatch { get => currentMatch; set => currentMatch = value; }

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
        
    }
}
