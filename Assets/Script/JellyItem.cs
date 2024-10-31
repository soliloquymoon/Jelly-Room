using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JellyItem : MonoBehaviour
{
    public Jelly jelly;
    public Image image;
    int code;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Image priceUnitImage;
    public GameManager gameManager;
    public GameObject lockImage;
    public ShopManager shopManager;

    public void SetItem(Jelly jelly, int code, Sprite jellySprite, Sprite unitIcon) {
        this.jelly = jelly;
        this.image.sprite = jellySprite;
        this.code = code;
        this.nameText.text = jelly.name;
        this.descriptionText.text = jelly.jelatine + "<color=green>J</color> / click";
        this.priceUnitImage.sprite = unitIcon;
        this.priceText.text = jelly.price.ToString();
        this.lockImage.SetActive(true);
    }

    public void PurchaseButton() {
        shopManager.Purchase(code);
    }

    public void UnlockJelly() {
        this.lockImage.SetActive(false);
    }
}
