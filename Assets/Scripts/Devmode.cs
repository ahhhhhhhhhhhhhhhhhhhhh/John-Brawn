using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devmode : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start ()
    {
        panel.gameObject.SetActive(false);
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
}
