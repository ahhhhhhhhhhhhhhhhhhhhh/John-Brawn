using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelSelector : MonoBehaviour {

    private LevelInfo levelToLoad;

    //prevents object from being duplicated everytime the scene loads
    private static LevelSelector nonDuplicateInstance;
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

    void Start()
    {
        loadCityPrefabs();
    }

    void loadCityPrefabs() {
        GameObject[] cities = Resources.LoadAll<GameObject>("Cities");
        Transform cityparent = GameObject.Find("Canvas").transform.Find("Cities");

        foreach (GameObject city in cities)
        {
            Vector3 location = city.transform.position;
            GameObject instantiatedCity = Instantiate(city, location, Quaternion.identity);
            instantiatedCity.transform.parent = cityparent;
        }
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
