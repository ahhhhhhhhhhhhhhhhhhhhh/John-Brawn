using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    public int score { get; private set; }
    public int zombiesKilled { get; private set; }

    public int reputation { get; private set; }
    private float reputationProgress; //progress to next reputation level
    public static readonly float[] reputationXP =
    {
        100,
        125,
        175,
        250,
        400,
        750,
        1500,
    };
    public static readonly string[] reputationLevels =
    {
        "Unheard-Of",
        "Inexperienced",
        "Village Defender",
        "Small Town Savoir",
        "Respected Contractor",
        "Major City Defender",
        "World Famous Hero",
        "Eternal Legend"
    };

    private LevelData levelData;
    private LevelSelectorCity city;
    private List<string> completedCities;
    private string currentCityName;
    private bool won;

    private GameObject statsPanel;
    private GameObject reputationPanel;

    //prevents object from being duplicated everytime the scene loads
    private static PlayerData nonDuplicateInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (nonDuplicateInstance == null)
        {
            nonDuplicateInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        completedCities = new List<string>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level")
        {
            levelData = GameObject.Find("Level Control").GetComponent<LevelData>();
        }
        else if (scene.name == "Level Selector")
        {
            statsPanel = GameObject.Find("Stats Panel");
            reputationPanel = GameObject.Find("Reputation Panel");

            updateStatsPanel();
            updateReputationPanel();

            if (won)
            {
                completedCities.Add(currentCityName);
            }

            //necessary because cities are deleted when scene is exited and so don't remember their state
            foreach (string cityName in completedCities)
            {
                LevelSelectorCity completedCity = getCity(cityName);
                completedCity.setState(LevelSelectorCity.State.Completed); 
            }
        }
    }

    //pass in true if player wins level, pass false if player loses
    public void LogData(bool win)
    {
        if (win)
        {
            won = true;

            score += levelData.money;
            zombiesKilled += levelData.zombiesKilled;
            reputationProgress += city.reward;

            checkLevelUp();
        }
    }

    //returns city of given name. necessary because city reference gets wiped when going to level scene
    private LevelSelectorCity getCity(string name)
    {
        if (SceneManager.GetActiveScene().name == "Level Selector")
        {
            Transform citiesParent = GameObject.Find("Cities").transform;
            for (int i = 0; i < citiesParent.childCount; i++)
            {
                LevelSelectorCity city = citiesParent.GetChild(i).GetComponent<LevelSelectorCity>();
                if (city.cityName == name)
                {
                    return city;
                }
            }
        }
        return null;
    }

    //checks if reputation progress is high enough to level up
    private void checkLevelUp()
    {
        while (reputation < reputationLevels.Length - 1 && reputationProgress >= reputationXP[reputation])
        {
            reputationProgress -= reputationXP[reputation];
            reputation++;
        }
    }

    public void setCity(LevelSelectorCity city)
    {
        this.city = city;
        currentCityName = city.cityName;
        won = false;
    }

    private void updateReputationPanel()
    {
        Text reputationText = reputationPanel.transform.Find("Reputation Text").GetComponent<Text>();
        Text nextText = reputationPanel.transform.Find("Next Text").GetComponent<Text>();
        Text XpText = reputationPanel.transform.Find("XP Text").GetComponent<Text>();
        Image progressBar = reputationPanel.transform.Find("Progress Bar Background").GetChild(0).GetComponent<Image>();

        reputationText.text = "\"" + reputationLevels[reputation] + "\"";
        if (reputation < reputationLevels.Length - 1)
        {
            nextText.text = "\"" + reputationLevels[reputation + 1] + "\"";
            XpText.text = reputationProgress + " / " + reputationXP[reputation];
            progressBar.transform.localScale = new Vector3(reputationProgress / reputationXP[reputation], 1, 1);
        }
        else
        {
            nextText.text = "";
            XpText.text = "";
            progressBar.transform.localScale = new Vector3(1, 1, 1);

            reputationPanel.transform.Find("Next Title").GetComponent<Text>().text = "";
        }
        
    }

    private void updateStatsPanel()
    {
        Text scoreText = statsPanel.transform.Find("Score Panel").transform.Find("Score Text").GetComponent<Text>();
        Text zombiesKilledText = statsPanel.transform.Find("Zombie Panel").transform.Find("Zombie Text").GetComponent<Text>();

        scoreText.text = "Score: " + score;
        zombiesKilledText.text = "Zombies Killed: " + zombiesKilled;
    }

    //for devmode
    public void addRepuationProgress(float added)
    {
        reputationProgress += added;
        checkLevelUp();
        updateReputationPanel();
    }
}
