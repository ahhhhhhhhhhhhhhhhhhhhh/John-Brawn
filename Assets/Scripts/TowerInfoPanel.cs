using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour {

    [Header("Unity Setup")]
    public Text typeText;
    public Text levelText;
    public Text damageText;
    public Text fireRateText;
    public Text rangeText;
    public GameObject upgradeButton;

    public BuildingManager buildManager;

	// Use this for initialization
	void Start () {
          
	}
	
	// Update is called once per frame
	void Update () {
        HideIfClickedOutside(gameObject);
	}

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html
    //thanks anisabboud
    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            buildManager.closeInfoPanel();
        }
    }

    public void loadTowerInfo(Tower tower)
    {
        TowerInfo info = tower.getProperties();
        typeText.text = tower.type;

        this.transform.position = tower.getLocation();

        levelText.text = "Level " + (tower.getLevel() + 1);
        damageText.text = "Damage: " + info.damage;
        fireRateText.text = "Fire Rate: " + info.fireRate + "/sec";
        rangeText.text = "Range: " + info.range.ToString();

        if (tower.getLevel() >= tower.properties.Length - 1) //means tower is max level
        {
            upgradeButton.GetComponent<Image>().enabled = false;
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Max Level";
        }
        else
        {
            upgradeButton.GetComponent<Image>().enabled = true;
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Upgrade";
        }
    }
}
