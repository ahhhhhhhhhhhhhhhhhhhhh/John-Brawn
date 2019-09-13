using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    public int score { get; private set; }
    public int reputation { get; private set; }

    private GameObject statsPanel;

    private LevelData levelData;
    private LevelSelectorCity city;

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
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level")
        {
            levelData = GameObject.Find("Level Control").GetComponent<LevelData>();
        }
        if (scene.name == "Level Selector")
        {
            statsPanel = GameObject.Find("Stats Panel");
        }
    }

    //pass in true if player wins level, pass false if player loses
    public void LogData(bool win)
    {
        score += levelData.money;
        reputation += city.reward;
    }

    public void setCity(LevelSelectorCity city)
    {
        this.city = city;
    }
}
