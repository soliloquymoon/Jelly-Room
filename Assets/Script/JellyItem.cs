using System;
using UnityEngine;
using UnityEngine.UI;

public class JellyItem : MonoBehaviour
{
    Jelly jelly;
    public Image image;
    int code;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Image priceUnitImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setItem(Jelly jelly, int code, Sprite jellySprite, Sprite unitIcon) {
        this.jelly = jelly;
        this.image.sprite = jellySprite;
        this.code = code;
        this.nameText.text = jelly.name;
        this.descriptionText.text = jelly.jelatine + "<color=green>J</color> / click";
        this.priceUnitImage.sprite = unitIcon;
        this.priceText.text = jelly.price.ToString();
    }

    public void purchaseButton() {
        Debug.Log(jelly.name);
    }
}
