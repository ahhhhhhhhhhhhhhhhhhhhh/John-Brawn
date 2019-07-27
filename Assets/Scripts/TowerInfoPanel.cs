using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour {

    [Header("Unity Setup")]
    public Text typeText;
    public Text damageText;
    public Text fireRateText;
    public Text rangeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadTowerInfo(TowerInfo info)
    {
        typeText.text = info.type;
        damageText.text = "Damage: " + info.damage;
        fireRateText.text = "Fire Rate: " + info.fireRate + "/sec";
        rangeText.text = "Range: " + info.range.ToString();
    }
}
