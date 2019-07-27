using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

    public GameObject panel;

    private bool paused;

	// Use this for initialization
	void Start () {

        panel.gameObject.SetActive(false);
        paused = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!paused && (Input.GetKeyDown("p") || Input.GetKeyDown("escape"))) {
            Time.timeScale = 0;
            panel.gameObject.SetActive(true);
        }

        if (paused && (Input.GetKeyDown("p") || Input.GetKeyDown("escape")))
        {
            Time.timeScale = 1;
            panel.gameObject.SetActive(false);
        }

        //setting paused has to be pulled out so that the previous section can work its stuff
        if (panel.gameObject.active)
        {
            paused = true;
        }
        else {
            paused = false;
        }

    }
}
