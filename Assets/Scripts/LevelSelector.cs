using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public LevelInfo[] levels;
    private LevelInfo levelToLoad;

    void OnEnable()
    {
        Object.DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void loadLevel(string name)
    {
        foreach (LevelInfo level in levels)
        {
            if (level.name == name)
            {
                levelToLoad = level;
            }
        }
        SceneManager.LoadScene("Level");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level")
        {
            LevelControl levelControl = GameObject.Find("Level Control").GetComponent<LevelControl>();
            levelControl.loadLevel(levelToLoad);
        }
    }
}
