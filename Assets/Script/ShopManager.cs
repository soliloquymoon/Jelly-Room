using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jelly
{
    public string name;
    public int jelatine;
    public int price;
    public char unit;
    public float speed;

    public Jelly(string name, int jelatine, int price, char unit, float speed) {
        this.name = name;
        this.jelatine = jelatine;
        this.price = price;
        this.unit = unit;
        this.speed = speed;
    }
}

public class ShopManager : MonoBehaviour
{
    public GameManager gameManager;
    public Sprite[] jellySprites;
    public Sprite[] priceUnitIcons;
    public JellyItem[] jellyItems;
    public GameObject itemPrefab;
    public GameObject shopContent;
    public GameObject shopWindow;
    int idx = 0;

    private Jelly[] jellies = new Jelly[]{
        new Jelly("Green Jelly", 1, 100, 'J', 10),
        new Jelly("Jelly Bean", 3, 200, 'J', 15),
        new Jelly("Grape Jelly", 10, 500, 'G', 5),
        new Jelly("Gummy Bear", 20, 1000, 'J', 10),
        new Jelly("Pudding Jelly", 50, 2000, 'J', 20),
        new Jelly("Chocolate Jelly", 100, 4000, 'G', 15),
        new Jelly("Cat Jelly", 200, 10000, 'J', 20),
        new Jelly("Sour Gummy", 300, 10000, 'G', 5),
        new Jelly("Shark Jelly", 800, 15000, 'J', 30),
        new Jelly("Sushi Jelly", 1000, 15000, 'G', 10),
        new Jelly("Yogurt Jelly", 1500, 20000, 'J', 10),
        new Jelly("Earth Jelly", 3000, 100000, 'J', 5)
    };

    // Start is called before the first frame update
    void Start()
    {        
        shopWindow.SetActive(false);
        jellyItems = new JellyItem[12];
        itemPrefab.GetComponent<JellyItem>()
            .SetItem(jellies[0], 0, jellySprites[0], priceUnitIcons[0]);
        jellyItems[0] = itemPrefab.GetComponent<JellyItem>();
        UnlockItem();
    }

    void UnlockItem() {
        idx++;
        GameObject newItem = Instantiate(itemPrefab, shopContent.transform);
        int priceUnit;
        jellyItems[idx] = newItem.GetComponent<JellyItem>();
        if (jellies[idx].unit.Equals('J'))
            priceUnit = 0;
        else
            priceUnit = 1;
        jellyItems[idx].SetItem(jellies[idx], idx, jellySprites[idx], priceUnitIcons[priceUnit]);
        jellyItems[idx - 1].UnlockJelly();
    }
    
    public void OpenShop() {
        shopWindow.SetActive(true);
    }

    public void Purchase(int code) {
        Debug.Log(code);
        Debug.Log(idx);
        Debug.Log(code == idx-1);
        if(code == idx - 1)
            UnlockItem();
        gameManager.AddJelly(code);
    }
}


