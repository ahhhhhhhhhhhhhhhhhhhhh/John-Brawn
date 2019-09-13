using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devmode : MonoBehaviour {

    public GameObject panel;

    private LevelData player;
    //private WaveController waveControl;
    //private BuildingManager buildManager;

	// Use this for initialization
	void Start ()
    {
        panel.gameObject.SetActive(false);

        player = gameObject.GetComponent<LevelData>();
        //waveControl = gameObject.GetComponent<WaveController>();
        //buildManager = gameObject.GetComponent<BuildingManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            if (panel.gameObject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
	}

    public void Show()
    {
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }

    public void addMoney(int added)
    {
        player.addMoney(added);
    }

    public void addLives(int added)
    {
        player.addLives(added);
    }
}
