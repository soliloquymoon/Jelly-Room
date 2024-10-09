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

    public Jelly(string name, int jelatine, int price, char unit) {
        this.name = name;
        this.jelatine = jelatine;
        this.price = price;
        this.unit = unit;
    }
}

public class ShopManager : MonoBehaviour
{
    public Sprite[] jellySprites;
    public Sprite[] priceUnitIcons;
    public JellyItem[] jellyItems;
    public GameObject itemPrefab;
    public GameObject shopContent;
    public GameObject shopWindow;

    // Start is called before the first frame update
    void Start()
    {
        shopWindow.SetActive(false);
        createItems();
        jellyItems = new JellyItem[12];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createItems() {
        Jelly[] jellies = new Jelly[]{new Jelly("Green Jelly", 1, 100, 'J'),
        new Jelly("Jelly Bean", 3, 200, 'J'),
        new Jelly("Grape Jelly", 10, 500, 'G'),
        new Jelly("Gummy Bear", 20, 1000, 'J'),
        new Jelly("Pudding Jelly", 50, 2000, 'J'),
        new Jelly("Chocolate Jelly", 100, 4000, 'G'),
        new Jelly("Cat Jelly", 200, 10000, 'J'),
        new Jelly("Sour Gummy", 300, 10000, 'G'),
        new Jelly("Shark Jelly", 800, 15000, 'J'),
        new Jelly("Sushi Jelly", 1000, 15000, 'G'),
        new Jelly("Yogurt Jelly", 1500, 20000, 'J'),
        new Jelly("Earth Jelly", 3000, 100000, 'J')
        };

        for (int i = 0; i < jellies.Length; i++) {
            GameObject newItem = Instantiate(itemPrefab, shopContent.transform);
            int priceUnit;
            jellyItems[i] = newItem.GetComponent<JellyItem>();
            if (jellies[i].unit.Equals('J'))
                priceUnit = 0;
            else
                priceUnit = 1;
            jellyItems[i].setItem(jellies[i], i, jellySprites[i], priceUnitIcons[priceUnit]);
        }
        Destroy(itemPrefab);
    }
    
    public void OpenShop() {
        shopWindow.SetActive(true);
    }
}


