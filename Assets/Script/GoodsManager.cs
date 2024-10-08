using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoodsManager : MonoBehaviour
{
    int money;
    int jelatine;
    int maximum = 1000000000;

    public Text moneyText;
    public Text jelatineText;
    void Start()
    {
        money = 0;
        jelatine = 9999;
        setMoney(getMoney());
        setJelatine(getJelatine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMoney(int money) {
        this.money = money;
        moneyText.text = this.money.ToString();
    }

    public int getMoney() {
        return this.money;
    }

    public void setJelatine(int jelatine) {
        // if (jelatine >= 1000000000)
        // TODO: add message
        this.jelatine = jelatine;
        if (jelatine < 1000) // 0 - 999
            jelatineText.text = this.jelatine.ToString();
        else if (jelatine < 1000000) // 1,000 - 999,999	
            jelatineText.text = this.jelatine.ToString("#,###");
        else // 1,000,000 - 1,000,000,000
            jelatineText.text = this.jelatine.ToString("#,###,###");
            
    }

    public int getJelatine() {
        return this.jelatine;
    }
}
