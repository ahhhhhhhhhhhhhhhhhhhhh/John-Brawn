using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorCity : MonoBehaviour {

    public enum State
    {
        Locked,
        Open,
        Completed
    }

    [Header("City Properties")]
    public string cityName;
    public int reward; //reputation gained by beating level
    public int reputation; //reputation required to attempt level
    public State state;
    public LevelInfo levelInfo;

    [Header("Unity Setup")]
    public Sprite[] images;
    public GameObject PopupPrefab;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(display_properties));

        PlayerData playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();
        if (playerData.reputation >= reputation)
        {
            setState(State.Open);
        }
        else
        {
            setState(State.Locked);
        }
	}
	
    public void setState(State newState)
    {
        state = newState;
        GetComponent<Image>().sprite = images[(int)state];
    }

    public void display_properties()
    {
        GameObject popup = Instantiate(PopupPrefab);
        popup.transform.position = transform.position;
        popup.GetComponent<PopupCityInspector>().city = this;
        GameObject canvas = GetComponentInParent<Canvas>().gameObject;
        popup.transform.SetParent(canvas.transform);
    }
}
