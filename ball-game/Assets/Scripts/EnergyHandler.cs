using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHandler : MonoBehaviour
{
    public static EnergyHandler INSTACE;

    [SerializeField] float regenerationRate = 0.5f;
    List<float> energyCosts;
    float[] energies;
    bool isRoundStarted = false;

    private void Awake()
    {
        INSTACE = this;

        MyEventSystem.INSTANCE.OnRoundStart += OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd += OnRoundEnd;
    }

    // Start is called before the first frame update
    void Start()
    {
        energies = new float[2];

        float[] defaultEnergyCost = { 2, 3 };
        energyCosts = new List<float>(defaultEnergyCost);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRoundStarted)
            Regenerate();
    }

    void Regenerate()
    {
        for(int i = 0; i < 2; i++)
        {
            energies[i] += regenerationRate * Time.deltaTime;
            energies[i] = Mathf.Clamp(energies[i], 0, 5);
        }
    }

    public bool TrySpentEnergy(int owner, float amount)
    {
        if(energies[owner] >= amount)
        {
            energies[owner] -= amount;
            return true;
        }

        return false;
    }

    public void OnRoundStart()
    {
        energies = new float[2];
        isRoundStarted = true;
    }

    public void OnRoundEnd()
    {
        isRoundStarted = false;
    }

    private void OnDisable()
    {
        MyEventSystem.INSTANCE.OnRoundStart -= OnRoundStart;
        MyEventSystem.INSTANCE.OnRoundEnd -= OnRoundEnd;
    }
}
