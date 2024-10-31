using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int money;
    int jelatine;
    int maximum = 1000000000;

    public Text moneyText;
    public Text jelatineText;
    public GameObject canvas;
    public GameObject jellyObjectPrefab;
    public ShopManager shopManager;
    void Start()
    {
        money = 10000;
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

    public void AddJelly(int code) {
        Jelly jellyType = shopManager.jellyItems[code].jelly;
        GameObject newJelly = Instantiate(jellyObjectPrefab, canvas.transform);
        newJelly.GetComponent<JellyObject>().SetJellyObject(jellyType, shopManager.jellySprites[code]);
        if (jellyType.unit == 'J') {
            if (this.jelatine - jellyType.price >= 0) {
                setJelatine(this.jelatine - jellyType.price);
                shopManager.shopWindow.SetActive(false);
            }
            //TODO: else
        }
        else {
            if (this.money - jellyType.price >= 0) {
                setMoney(this.money - jellyType.price);
                shopManager.shopWindow.SetActive(false);
            }
            //TODO: else
        }
    }

    public void SellJelly(GameObject jellyObj) {
        setMoney(this.money + jellyObj.GetComponent<JellyObject>().sellPrice);
        Destroy(jellyObj);
    }
}
