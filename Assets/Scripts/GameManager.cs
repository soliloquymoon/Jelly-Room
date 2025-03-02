using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int gold; // Current amount of gold the player has
    private int jelatine; // Current amount of jelatine the player has
    private const int MAXIMUM = 1000000; // Maximum limit for gold and jelatine

    private int capacityLv; // Capacity lv (number of jellies that can be stored)
    private int productibilityLv; // Productibility (jelatine bonus)

    // UI Text components for displaying gold and jelatine amounts
    public Text goldText;
    public Text jelatineText;

    // UI GameObject for displaying error messages
    public GameObject errorMsg;

    private JellyObject[] jellyObjects = new JellyObject[6];

    void Start()
    {
        // Load information from playerprefs
        gold = PlayerPrefs.GetInt("Gold", 0);
        jelatine = PlayerPrefs.GetInt("Jelatine", 300);
        capacityLv = PlayerPrefs.GetInt("Capacity", 1);
        productibilityLv = PlayerPrefs.GetInt("Productibility", 1);

        SetGold(this.gold);
        SetJelatine(this.jelatine);

        errorMsg.SetActive(false);
        errorMsg.GetComponentInChildren<Button>().onClick.AddListener(() => errorMsg.SetActive(false));
    }

    public void AddJellyObject(JellyObject obj, int idx = -1)
    {
        if (idx == -1)
        {
            for (int i = 0; i < jellyObjects.Length; i++)
            {
                if (jellyObjects[i] == null)
                {
                    idx = i;
                    break;
                }
            }
        }
        jellyObjects[idx] = obj;
        obj.arrayIdx = idx;
    }

    public int GetCapacityLv()
    {
        return this.capacityLv;
    }

    public void UpgradeCapacity()
    {
        this.capacityLv += 1;
    }

    public int GetProductibilityLv()
    {
        return this.productibilityLv;
    }
    
    public void UpgradeProductibility()
    {
        this.productibilityLv += 1;
    }

    private void SetGold(int gold) {
        this.gold = gold >= MAXIMUM ? MAXIMUM : gold;
        goldText.text = this.gold == 0 ? "0" : this.gold.ToString("#,###,###");
    }

    private void SetJelatine(int jelatine) {
        this.jelatine = jelatine >= MAXIMUM ? MAXIMUM : jelatine;
        jelatineText.text = this.jelatine == 0 ? "0" : this.jelatine.ToString("#,###,###");
    }

    public bool UpdateJelatine(int amt) {
        if (jelatine + amt < 0)
        {
            SetErrorMsg("Not enough jelatine!", "TIP: Click jellies to earn more jelatines");
            return false;
        }
        SetJelatine(this.jelatine + amt);
        return true;
    }

    public bool UpdateGold(int amt) {
        if (gold + amt < 0)
        {
            SetErrorMsg("Not enough gold!", "TIP: Sell jellies to earn more gold");
            return false;
        }
        
        SetGold(this.gold + amt);
        return true;
    }

    public void SetErrorMsg(string error, string tip)
    {
        Text[] msg = errorMsg.GetComponentsInChildren<Text>();
        msg[0].text = error; // Set error message
        msg[1].text = tip; // Set tip message
        errorMsg.SetActive(true);
        AudioManager.instance.PlaySFX("Fail");
    }

    // Save PlayerPrefs when the application is closed or moves to background
    void OnApplicationQuit()
    {
        // Jelatine and Gold
        PlayerPrefs.SetInt("Jelatine", this.jelatine);
        PlayerPrefs.SetInt("Gold", this.gold);

        // Upgrade status
        PlayerPrefs.SetInt("Capacity", this.capacityLv);
        PlayerPrefs.SetInt("Productibility", this.productibilityLv);

        // Data of jellies
        foreach(JellyObject obj in jellyObjects)
        {
            if (obj != null)
                obj.SaveData();
        }

        // Save changes
        PlayerPrefs.Save();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) 
        {
            PlayerPrefs.Save();
        }
    }
}
