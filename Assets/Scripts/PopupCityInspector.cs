using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCityInspector : MonoBehaviour {

    public LevelSelectorCity city;

    private LevelSelector level_selector;
    private PlayerData playerData;

	void Start ()
    {
        level_selector = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();

        renderProperties();

        Button startButton = transform.Find("Play Button").gameObject.GetComponent<Button>();

        if (city.state == LevelSelectorCity.State.Open)
        {
            startButton.onClick.AddListener(new UnityEngine.Events.UnityAction(start_game));
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Play";
            startButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            startButton.interactable = false;
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Locked";
            startButton.GetComponent<Image>().color = Color.grey;
        }

        GameObject quitButton = transform.Find("Quit Button").gameObject;
        quitButton.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(quit));
    }

    void start_game()
    {
        playerData.setCity(city);
        level_selector.loadLevel(city.levelInfo);
    }

    void quit()
    {
        Destroy(gameObject);
    }

    public void renderProperties()
    {
        Text prop_text = transform.Find("Info").GetComponent<Text>();
        Text title = transform.Find("Title").GetComponent<Text>();
        string properties = "Reward: " + city.reward + "\nLength: " + city.levelInfo.waves.Length + " waves\nReputation: Level " + city.reputation;
        prop_text.text = properties;
        title.text = city.cityName;
    }
}
