using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler INSTANCE;

    [SerializeField] GameObject roundEnd = null;
    [SerializeField] GameObject gameEnd = null;
    [SerializeField] GameObject needPenalty = null;

    [SerializeField] TMPro.TMP_Text roundText = null;
    [SerializeField] TMPro.TMP_Text gameOverText = null;

    private void Awake()
    {
        INSTANCE = this;
    }

    public void StartGame()
    {
        GameManager.INSTANCE.TryStartGame();
    }

    public void RoundEnd(Owner _winner)
    {
        roundEnd.SetActive(true);

        string text;
        if(_winner == Owner.Player1)
        {
            text = "Player 1 Win";
        }
        else if(_winner == Owner.Player2)
        {
            text = "Player 2 Win";
        }
        else
        {
            text = "Time Out!";
        }

        roundText.text = text;
    }

    public void GameOver(bool _needPenalty)
    {
        if (_needPenalty)
        {
            needPenalty.SetActive(true);
        }
        else
        {
            gameEnd.SetActive(true);

            string text;
            if(GameManager.INSTANCE.CurrentScore > 0)
            {
                text = "Player 1 Win";
            }
            else
            {
                text = "Player 2 Win";
            }
            gameOverText.text = text;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
