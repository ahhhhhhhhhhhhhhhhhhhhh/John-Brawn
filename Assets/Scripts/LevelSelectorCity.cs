using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorCity : MonoBehaviour {

    public enum State {
        Locked,
        Open,
        Completed
    }

    [Header("City Properties")]
    public string cityName;
    public int reward;
    public int reputation;
    public State state;
    public LevelInfo levelInfo;

    [Header("Unity Setup")]
    public Sprite[] images;
    public GameObject PopupPrefab;

	// Use this for initialization
	void Start ()
    {
        name = cityName;
        setState(state);
        GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(display_properties));
	}
	
    public void setState(State new_state)
    {
        state = new_state;
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
