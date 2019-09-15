using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCityInspector : MonoBehaviour {

    public LevelSelectorCity city;

    private LevelSelector levelSelector;
    private PlayerData playerData;

	void Start ()
    {
        levelSelector = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();

        DisplayInfo();

        Button startButton = transform.Find("Play Button").gameObject.GetComponent<Button>();

        if (city.state == LevelSelectorCity.State.Open)
        {
            startButton.interactable = true;
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Play";
            startButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            startButton.interactable = false;
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Locked";
            startButton.GetComponent<Image>().color = Color.grey;
        }
    }

    public void StartGame()
    {
        playerData.setCity(city);
        levelSelector.loadLevel(city.levelInfo);
    }

    public void Quit()
    {
        Destroy(gameObject);
    }

    public void DisplayInfo()
    {
        Text title = transform.Find("Title").GetComponent<Text>();
        title.text = city.cityName;

        Transform infoPanel = transform.Find("Info");
        Text rewardText = infoPanel.Find("Reward Text").GetComponent<Text>();
        Text requirmentText = infoPanel.Find("Requirment Text").GetComponent<Text>();
        Text wavesText = infoPanel.Find("Waves Text").GetComponent<Text>();

        rewardText.text = "Defending this city will increase your reputation by " + city.reward;
        requirmentText.text = "This city will only hire you if you are " + PlayerData.reputationLevels[city.reputation] + " or better";
        wavesText.text = "The contract is for " + city.levelInfo.waves.Length + " days";
    }
}
