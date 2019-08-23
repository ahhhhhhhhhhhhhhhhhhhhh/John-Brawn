using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCityInspector : MonoBehaviour {

    public LevelSelectorCity city;
    private LevelSelector level_selector;

	void Start ()
    {
        level_selector = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        renderProperties();
        Button startButton = transform.Find("Play Button").gameObject.GetComponent<Button>();

        if (city.state == LevelSelectorCity.State.Open)
        {
            startButton.onClick.AddListener(new UnityEngine.Events.UnityAction(start_game));
        }
        else
        {
            startButton.interactable = false;
        }

        GameObject quitButton = transform.Find("Quit Button").gameObject;
        quitButton.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(quit));
    }

    void start_game()
    {
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
        string properties = "Payroll: " + city.reward + "\nLength: " + city.levelInfo.waves.Length + " waves\nReputation: Level " + city.reputation;
        prop_text.text = properties;
        title.text = city.cityName;
    }
}
