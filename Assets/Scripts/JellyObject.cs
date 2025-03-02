using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JellyObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image img;
    public Text lvText;
    public int arrayIdx;

    private GameManager gameManager;
    private float waitTime;
    private Animator animator;
    private Vector2 newPosition;
    private Vector2 posBeforeDrag;

    private RectTransform rectTransform;
    private Rect parentRect;


    // Load from saved data
    private Jelly jellyType;
    private int level;
    private int exp;

    // Calculate from saved data
    private int sellPrice;
    private int nextLvUpExp;

    // Area that this object can move around
    private float x;
    private float y;


    void Start()
    {
        // Start at random position
        this.rectTransform.anchoredPosition = new Vector2(Random.Range(-x, x), Random.Range(-y, y));
        this.newPosition = rectTransform.anchoredPosition;
        this.waitTime = 0;
    }
 
    void Update()
    {
        animator.SetBool("walk", true);
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, newPosition, jellyType.speed * Time.deltaTime);
        if (Vector2.Distance(rectTransform.anchoredPosition, newPosition) < 0.1f)
        {
            animator.SetBool("walk", false);
            if (waitTime <= 0)
            {
                newPosition = new Vector2(Random.Range(-x, x), Random.Range(-y, y));
                waitTime = Random.Range(0, 5);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(rectTransform.anchoredPosition.x - newPosition.x > 0) {
            img.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            img.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Called before Start()
    public void SetJellyObject(Jelly jellyType, Sprite jellySprite, int level = 1, int exp = 0)
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.x = this.transform.parent.GetComponent<RectTransform>().rect.size.x / 2;
        this.y = this.transform.parent.GetComponent<RectTransform>().rect.size.y / 2;
        this.gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        this.animator = this.GetComponentInChildren<Animator>();
        this.animator.SetFloat("speed", jellyType.speed);
        
        this.jellyType = jellyType;
        this.img.sprite = jellySprite;
        this.sellPrice = jellyType.unit == 'J' ? jellyType.price : jellyType.price * 2;
        this.level = level;
        this.exp = exp;
        this.nextLvUpExp = level * 15;
        this.lvText.text = level >= 5 ? "Max Lv" : $"Lv {level}";

        for (int i = 1; i < level; i++)
            UpdateSize();
    }

    public int GetLevel()
    {
        return this.level;
    }

    public int GetSellPrice()
    {
        return this.sellPrice;
    }

    public void OnClick() {
        gameManager.UpdateJelatine(this.jellyType.jelatine * gameManager.GetProductibilityLv());

        if (level < 5)
        {
            exp++;
            if(exp >= nextLvUpExp) {
                level++;
                UpdateSize();
                nextLvUpExp = level * 10;
                lvText.text = level >= 5 ? "Max Lv" : $"Lv {level}";
                exp = 0;
                AudioManager.instance.PlaySFX("Unlock");
                return;
            }
        }
        AudioManager.instance.PlaySFX("Grow");
    }

    private void UpdateSize()
    {
        float scale = 1.1f;
        Vector2 newSize = new Vector2(rectTransform.rect.width * scale, rectTransform.rect.height * scale);
        
        rectTransform.sizeDelta = newSize;
        this.GetComponent<CapsuleCollider2D>().size = newSize;
}

    public void SaveData()
    {
        PlayerPrefs.SetString($"Jelly{arrayIdx}", $"{jellyType.name}*{level}*{exp}");
    }

    private bool IsOutsideParentArea()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        // Half width & height of parent(Room) and this jelly object
        float halfWidth = rectTransform.rect.size.x / 2;
        float halfHeight = rectTransform.rect.size.y / 2;

        return pos.x - halfWidth < -x || pos.x + halfWidth > x ||
        pos.y - halfHeight < -y || pos.y + halfHeight > y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.img.color = new Color(1, 1, 1, 0.5f);
        this.img.raycastTarget = false;
        this.GetComponentInChildren<Rigidbody2D>().simulated = false;
        posBeforeDrag = this.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.img.color = new Color(1, 1, 1, 1f);
        if (IsOutsideParentArea())
        {
            rectTransform.anchoredPosition = posBeforeDrag;
        }
        this.img.raycastTarget = true;
        this.GetComponentInChildren<Rigidbody2D>().simulated = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.img.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.img.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        newPosition = new Vector2(Random.Range(-x, x), Random.Range(-y, y));
    }
}
