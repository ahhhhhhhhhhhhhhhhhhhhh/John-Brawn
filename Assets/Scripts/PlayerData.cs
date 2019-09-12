using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    [Header("Unity Setup")]
    public Text moneyDisplay;
    public Text livesDisplay;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public GameObject Enemies;

    public int money { get; private set; }
    public int lives { get; private set; }
    private int zombiesKilled;

    private void Update()
    {
        moneyDisplay.text = "$" + money;
        livesDisplay.text = lives + " lives";

        if (lives <= 0)
        {
            gameOverMenu.SetActive(true);
        }

        if (Enemies.transform.childCount == 0 && GetComponent<WaveController>().Done())
        {
            winMenu.transform.Find("Zombies Killed Text").GetComponent<Text>().text = "You Killed " + zombiesKilled + " Zombies. Nice!";
            winMenu.SetActive(true);
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
