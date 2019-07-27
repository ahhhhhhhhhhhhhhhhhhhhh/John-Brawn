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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadTowerInfo(Tower tower)
    {
        TowerInfo info = tower.getProperties();
        typeText.text = tower.type;
        levelText.text = "Level " + (tower.getLevel() + 1);
        damageText.text = "Damage: " + info.damage;
        fireRateText.text = "Fire Rate: " + info.fireRate + "/sec";
        rangeText.text = "Range: " + info.range.ToString();
    }
}
