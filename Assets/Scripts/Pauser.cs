using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start ()
    {
        panel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!panel.gameObject.activeSelf && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Pause();
        }
        else if (panel.gameObject.activeSelf && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
    }

    public void loadLevelSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level Selector");
    }
}
