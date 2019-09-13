using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour {

    [Header("Unity Setup")]
    public Text moneyDisplay;
    public Text livesDisplay;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public GameObject enemies;

    public int money { get; private set; }
    public int lives { get; private set; }
    private int zombiesKilled;

    private void Update()
    {
        moneyDisplay.text = "$" + money;
        livesDisplay.text = lives + " lives";

        if (enemies.transform.childCount == 0 && GetComponent<WaveController>().Done() && !(winMenu.activeSelf || gameOverMenu.activeSelf))
        {
            TriggerWin();
        }

        if (lives <= 0 && !(winMenu.activeSelf || gameOverMenu.activeSelf))
        {
            TriggerLoss();
        }
    }

    public void TriggerWin()
    {
        winMenu.transform.Find("Zombies Killed Text").GetComponent<Text>().text = "You Killed " + zombiesKilled + " Zombies. Nice!";
        winMenu.SetActive(true);

        attemptLogData(true);
    }

    public void TriggerLoss()
    {
        gameOverMenu.SetActive(true);

        attemptLogData(false);
    }

    //attempts to find player data object and record level results
    private void attemptLogData(bool win)
    {
        GameObject playerData = GameObject.Find("Player Data");
        if (playerData != null)
        {
            playerData.GetComponent<PlayerData>().LogData(win);
        }
    }

    public void addMoney(int gain)
    {
        money += gain;
    }

    public void subtractMoney(int cost)
    {
        money -= cost;
    }

    public void addLives(int gain)
    {
        lives += gain;
    }

    public void subtractLives(int lost)
    {
        lives -= lost;
    }

    public void zombieKilled()
    {
        zombiesKilled++;
    }
}
