using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JellyObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image img;
    public GameManager gameManager;
    private float waitTime;
    private int minX = -400, maxX = 400;
    private int minY = -90, maxY = 90;
    private Animator animator;
    private RectTransform rect;
    private Vector2 newPosition;
    private Jelly jelly;
    private int level;
    public int sellPrice;
    private int exp;
    private int nextLvUp;
    private Vector2 posBeforeDrag;
    
    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        waitTime = 5;
        animator = this.GetComponent<Animator>();
        newPosition = rect.anchoredPosition;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator.SetFloat("speed", jelly.speed);
        level = 1;
        exp = 0;
        nextLvUp = 5;
    }
 
    void Update()
    {
        animator.SetBool("walk", true);
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition,
        newPosition, jelly.speed * Time.deltaTime);
        if (Vector2.Distance(rect.anchoredPosition, newPosition) < 0.1f)
        {
            animator.SetBool("walk", false);
            if (waitTime <= 0)
            {
                newPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = 5;
            }
            else
            {
                waitTime -= Time.deltaTime * Random.Range(0, 5);
            }
        }

        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(rect.anchoredPosition.x - newPosition.x > 0) {
            img.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            img.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void SetJellyObject(Jelly jelly, Sprite jellySprite) {
        this.jelly = jelly;
        this.img.sprite = jellySprite;
        this.sellPrice = jelly.price / 2;
    }

    public void OnClick() {
        gameManager.setJelatine(gameManager.getJelatine() + this.jelly.jelatine);
        if (level <= 5)
        {
            exp++;
            if(exp >= nextLvUp) {
                Debug.Log(jelly.name + "Level up");
                level++;
                rect.sizeDelta = new Vector2(rect.rect.width + 30, rect.rect.height + 30);
                img.rectTransform.sizeDelta = rect.sizeDelta;
                this.GetComponent<CapsuleCollider2D>().size = new Vector2(
                    rect.rect.width + 30, rect.rect.height + 30);
                nextLvUp += 1 * level; //TODO: change to 10
                Debug.Log(nextLvUp);
                exp = 0;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.img.color = new Color(1, 1, 1, 0.5f);
        this.img.raycastTarget = false;
        posBeforeDrag = this.rect.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.img.color = new Color(1, 1, 1, 1f);
        if (rect.anchoredPosition.x < minX || rect.anchoredPosition.x > maxX ||
        rect.anchoredPosition.y < minY || rect.anchoredPosition.y > maxY)
        {
            rect.anchoredPosition = posBeforeDrag;
        }
        this.img.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.img.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.img.color = Color.white;
    }
}
