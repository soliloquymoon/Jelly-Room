using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private int code;
    private Jelly jellyType;
    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Image priceUnitImage;
    public GameObject lockImage;
    private ShopManager shopManager;

    void Start()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }

    public void SetItem(Jelly jellyType, int code, Sprite jellySprite, Sprite unitIcon)
    {
        this.jellyType = jellyType;
        this.image.sprite = jellySprite;
        this.code = code;
        this.nameText.text = jellyType.name;
        this.descriptionText.text = $"{jellyType.jelatine}<color=green>J</color> / click";
        this.priceUnitImage.sprite = unitIcon;
        this.priceUnitImage.SetNativeSize();
        this.priceText.text = jellyType.price.ToString("###,### ");
        this.lockImage.SetActive(true);
        this.GetComponentInChildren<Button>().onClick.AddListener(() => shopManager.PurchaseJelly(this.code));
    }

    public void Unlock()
    {
        this.lockImage.SetActive(false);
    }
}
