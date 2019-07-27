using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCityInspector : MonoBehaviour {

    // Use this for initialization

    public LevelSelectorCity city;
    LevelSelector level_selector;

	void Start () {
        level_selector = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        renderProperties();
        Button startButton = transform.Find("Play Button").gameObject.GetComponent<Button>();
        if (city.state == 1)
        {
            startButton.onClick.AddListener(new UnityEngine.Events.UnityAction(start_game));
        } else
        {
            startButton.interactable = false;

        }

        GameObject quitButton = transform.Find("Quit Button").gameObject;
        quitButton.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(quit));


    }

    void start_game()
    {
        level_selector.loadLevel(city.city_name);
    }

    void quit()
    {
        Destroy(gameObject);
    }

    public void renderProperties()
    {
        Text prop_text = transform.Find("Info").GetComponent<Text>();
        Text title = transform.Find("Title").GetComponent<Text>();
        string properties = "Payroll: " + city.price + "\nLength: " + city.num_waves + " days\nReputation: Level " + city.reputation;
        prop_text.text = properties;
        title.text = city.city_name;
    }
	
}
