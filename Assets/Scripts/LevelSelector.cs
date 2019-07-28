using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    private LevelInfo levelToLoad;

    void OnEnable()
    {
        Object.DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void loadLevel(LevelInfo levelInfo)
    {
        levelToLoad = levelInfo;
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
