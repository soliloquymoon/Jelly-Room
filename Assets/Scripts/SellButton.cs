using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButton : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    private Text text;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        text = this.GetComponentInChildren<Text>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GameObject.Find("Room").transform.childCount > 2)
        {
            JellyObject jelly = eventData.pointerDrag.GetComponentInParent<JellyObject>();
            if (jelly.GetLevel() >= 5)
            {
                int price = jelly.GetSellPrice();
                gameManager.UpdateGold(price);
                PlayerPrefs.DeleteKey($"Jelly{jelly.arrayIdx}");
                Destroy(jelly.gameObject);
                AudioManager.instance.PlaySFX("Sell");
                StartCoroutine(ShowSellPrice(price));
            }
            else
            {
                gameManager.SetErrorMsg("This jelly isn't ready to be sold!", "TIP: Train your jelly to reach level 5");
            }
        }
        else
        {
            gameManager.SetErrorMsg("You cannot sell your last jelly!", "TIP: Buy more jellies to sell this jelly");
        }
    }

    private IEnumerator ShowSellPrice(int price)
    {
        this.text.text = $"+ {price}G";
        yield return new WaitForSeconds(1f);
        this.text.text = "";
    }
}
