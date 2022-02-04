using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    [SerializeField] List<TMPro.TMP_Text> playerNames = new List<TMPro.TMP_Text>();
    string player1Template = "Player 1 ";
    string player2Template = "Player 2 ";
    string attacking = "(Attacker)";
    string defending = "(Defender)";

    private void Awake()
    {
        MyEventSystem.INSTANCE.OnRoundStart += OnRoundStart;
    }

    public void OnRoundStart()
    {
        int currentMatch = GameManager.INSTANCE.CurrentMatch;
        if(currentMatch % 2 == 0)
        {
            playerNames[0].text = player1Template + attacking;
            playerNames[1].text = player2Template + defending;
        }
        else
        {
            playerNames[0].text = player1Template + defending;
            playerNames[1].text = player2Template + attacking;
        }
    }

    private void OnDestroy()
    {
        MyEventSystem.INSTANCE.OnRoundStart -= OnRoundStart;
    }
}
