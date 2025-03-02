using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    private GameManager gameManager;
    private Sprite[] jellySprites;
    private Sprite[] priceUnitIcons;
    public GameObject itemPrefab;
    public GameObject jellyObjectPrefab;
    public GameObject shopWindow;

    private List<Jelly> jellyTypes = new List<Jelly>{
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

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        shopWindow.SetActive(false);

        // Load Jelly sprites
        jellySprites = new Sprite[jellyTypes.Count];
        for(int i = 0; i < jellyTypes.Count; i++)
        {
            jellySprites[i] = Resources.Load<Sprite>("Sprites/InGame/Jelly " + i);
        }

        // Load price unit icons
        priceUnitIcons = new Sprite[2];
        priceUnitIcons[0] = Resources.Load<Sprite>("Sprites/UI/Icon 0");
        priceUnitIcons[1] = Resources.Load<Sprite>("Sprites/UI/Icon 1");

        // Instantiate shop items        
        int unlockedItem = PlayerPrefs.GetInt("UnlockedItems", 2);
        for (int i = 0; i < unlockedItem; i++)
            UnlockItem();

        // Load Jelly objects
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.HasKey($"Jelly{i}"))
            {
                GameObject newJelly = Instantiate(jellyObjectPrefab, GameObject.Find("Room").transform);
                string[] data = PlayerPrefs.GetString($"Jelly{i}").Split("*");
                int typeIdx = jellyTypes.FindIndex(type => type.name.Equals(data[0]));
                gameManager.AddJellyObject(newJelly.GetComponent<JellyObject>(), i);
                newJelly.GetComponent<JellyObject>().SetJellyObject(jellyTypes[typeIdx], jellySprites[typeIdx], int.Parse(data[1]), int.Parse(data[2]));
            }
        }
    }

    void UnlockItem() {
        int cnt = this.transform.childCount;

        // Add new unlocked item if there are more jelly items to unlock
        if (cnt < jellyTypes.Count)
        {
            ShopItem newItem = Instantiate(itemPrefab, this.transform).GetComponent<ShopItem>();
            int priceUnit = jellyTypes[cnt].unit.Equals('J') ? 0 : 1;
            newItem.SetItem(jellyTypes[cnt], cnt, jellySprites[cnt], priceUnitIcons[priceUnit]);
            newItem.name = jellyTypes[cnt].name;
        }

        // Unlock jelly item
        if (cnt != 0)
            this.transform.GetChild(cnt - 1).GetComponent<ShopItem>().Unlock();
        PlayerPrefs.SetInt("UnlockedItems", cnt + 1);
    }
    
    public void OpenShop() {
        AudioManager.instance.PlaySFX("Button");
        shopWindow.SetActive(true);
    }

    public void CloseShop() {
        shopWindow.SetActive(false);
    }

    public void PurchaseJelly(int code) {
        int jellyCnt = GameObject.Find("Room").transform.childCount - 1;
        // Check if the room is full
        if (jellyCnt >= gameManager.GetCapacityLv())
        {
            gameManager.SetErrorMsg("Your room is already full!", "TIP: Upgrade capacity to have more jellies");
        }
        else
        {
            Jelly jellyType = jellyTypes[code];

            // Update Jelatine or Money (according to the price unit)
            if (jellyType.unit == 'J') {
                if (!gameManager.UpdateJelatine(-jellyType.price)) return;
            }
            else {
                if (!gameManager.UpdateGold(-jellyType.price)) return;
            }

            // Instantiate new jelly
            GameObject newJelly = Instantiate(jellyObjectPrefab, GameObject.Find("Room").transform);
            gameManager.AddJellyObject(newJelly.GetComponent<JellyObject>());
            newJelly.GetComponent<JellyObject>().SetJellyObject(jellyType, jellySprites[code]);
            newJelly.name = jellyType.name;
            AudioManager.instance.PlaySFX("Buy");

            // Unlock new item if the purchased item is the latest unlocked item
            if(code == this.transform.childCount - 2)
            {
                UnlockItem();
            }
        }

        CloseShop();
    }
}


