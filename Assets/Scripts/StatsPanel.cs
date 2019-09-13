using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour {

    private PlayerData playerData;

    private void Start()
    {
        playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();
    }

    private void Update()
    {
        Text scoreText = transform.Find("Score Text").GetComponent<Text>();
        Text reputationText = transform.Find("Reputation Text").GetComponent<Text>();

        scoreText.text = "Score: " + playerData.score;
        reputationText.text = "Reputation: " + playerData.reputation;
    }
}
