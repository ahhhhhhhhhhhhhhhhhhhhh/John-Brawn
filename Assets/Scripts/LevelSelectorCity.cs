using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorCity : MonoBehaviour {

    public string city_name;
    public int price;
    public int reputation;
    public int state;
    public int num_waves;

    public Sprite[] images;

    public GameObject PopupPrefab;

	// Use this for initialization
	void Start () {
        name = city_name;
        setState(state);
        GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(display_properties));
	}
	
    public void setState(int new_state)
    {
        state = new_state;
        GetComponent<Image>().sprite = images[state];
    }

    public void display_properties()
    {
        if (state == 1)
        {
            GameObject popup = Instantiate(PopupPrefab);
            popup.transform.position = transform.position;
            popup.GetComponent<PopupCityInspector>().city = this;
            GameObject canvas = GetComponentInParent<Canvas>().gameObject;
            popup.transform.SetParent(canvas.transform);
        }
    }
}
