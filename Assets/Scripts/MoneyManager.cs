using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    [Header("Unity Setup")]
    public Text moneyDisplay;

    public int money { get; private set; }

    private void Update()
    {
        moneyDisplay.text = (money + "");
    }

    public void add(int gain)
    {
        money += gain;
    }

    public void subtract(int cost)
    {
        money -= cost;
    }
}
