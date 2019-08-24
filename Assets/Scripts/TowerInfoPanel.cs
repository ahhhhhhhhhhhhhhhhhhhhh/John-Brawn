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
    public LevelControl levelControl;

    public BuildingManager buildManager;

    private Tower selectedTower;

    // Update is called once per frame
    void Update ()
    {
        HideIfClickedOutside(gameObject);
	}

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html
    //thanks anisabboud
    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
            panel.GetComponent<RectTransform>(),
            Input.mousePosition, Camera.main))
        {
            buildManager.closeInfoPanel();
        }
    }

    public void setSelectedTower(Tower tower)
    {
        selectedTower = tower;
    }

    public void loadTowerInfo()
    {
        TowerInfo info = selectedTower.getProperties();
        typeText.text = selectedTower.type;

        this.transform.position = selectedTower.getLocation();

        levelText.text = "Level " + (selectedTower.getLevel() + 1);
        damageText.text = "Damage: " + info.damage;
        fireRateText.text = "Fire Rate: " + info.fireRate + "/sec";
        rangeText.text = "Range: " + info.range.ToString();

        if (selectedTower.getLevel() >= selectedTower.properties.Length - 1) //means tower is max level
        {
            upgradeButton.GetComponent<Image>().enabled = false;
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Max Level";
        }
        else
        {
            TowerInfo next = selectedTower.properties[selectedTower.getLevel() + 1];
            if (levelControl.getMoney() >= next.cost)
            {
                upgradeButton.GetComponent<Image>().enabled = true;
            }
            else
            {
                upgradeButton.GetComponent<Image>().enabled = false;
            }
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Upgrade ($" + next.cost + ")";
        }
    }

    public void showUpgrades()
    {
        if (selectedTower.getLevel() < selectedTower.properties.Length - 1)
        {
            TowerInfo current = selectedTower.getProperties();
            TowerInfo next = selectedTower.properties[selectedTower.getLevel() + 1];

            if (next.damage - current.damage > 0)
            {
                damageText.text += " + " + (next.damage - current.damage);
            }
            if (next.fireRate - current.fireRate > 0)
            {
                fireRateText.text += " + " + (next.fireRate - current.fireRate);
            }
            if (next.range - current.range > 0)
            {
                rangeText.text += " + " + (next.range - current.range);
            }
        }
    }
}
