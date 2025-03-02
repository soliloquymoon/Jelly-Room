using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private int[] upgradePrice = {0, 100, 250, 1000, 5000, 20000};
    private GameManager gameManager;

    public Text capacityText;
    public Text productibilityText;

    public Button capacityButton;
    public Button productibilityButton;

    void Start()
    {
        this.gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        this.gameObject.SetActive(false);

        UpdateTexts(capacityText, capacityButton, PlayerPrefs.GetInt("Capacity", 1));
        UpdateTexts(productibilityText, productibilityButton, PlayerPrefs.GetInt("Productibility", 1));
    }

    private void UpgradeCapacity()
    {
        int level = gameManager.GetCapacityLv();

        // Check if the user has enough jelatine
        if (gameManager.UpdateJelatine(-upgradePrice[level]))
        {
            gameManager.UpgradeCapacity();
            UpdateTexts(capacityText, capacityButton, level + 1);
            AudioManager.instance.PlaySFX("Unlock");
        }
    }

    private void UpgradeProductibililty()
    {
        int level = gameManager.GetProductibilityLv();

        // Check if the user has enough gold
        if (gameManager.UpdateGold(-upgradePrice[level]))
        {
            gameManager.UpgradeProductibility();
            UpdateTexts(productibilityText, productibilityButton, level + 1);
            AudioManager.instance.PlaySFX("Unlock");
        }
    }

    public void CloseUpgrade()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenUpgrade()
    {
        this.gameObject.SetActive(true);
    }

    private void UpdateTexts(Text detailText, Button button, int level)
    {
        Text priceText = button.GetComponentInChildren<Text>();
        if (level < upgradePrice.Length)
        {
            detailText.text = $"x{level} ▷ <color=red>x{level + 1}</color>";
            priceText.text = upgradePrice[level].ToString("###,### ");
        }
        else
        {
            detailText.text = "";
            priceText.text = " <color=white>MAX LV</color> ";
            button.interactable = false;
            // Remove price unit image
            Destroy(button.transform.GetChild(0).gameObject);
        }
    }
}