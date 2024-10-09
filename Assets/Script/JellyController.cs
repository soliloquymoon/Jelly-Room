using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JellyController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image img;
    public float speed;
    public float startWaitTime;
    public GameManager gameManager;
    private float waitTime;
    private int minX = -400, maxX = 400;
    private int minY = -90, maxY = 90;
    private Animator animator;
    private RectTransform rect;
    private Vector2 newPosition;
    private int jelatine;
    private int level;
    private int price;
    private int exp;
    private int nextLvUp;
    Vector2 posBeforeDrag;
    
    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        waitTime = startWaitTime;
        animator = this.GetComponent<Animator>();
        newPosition = rect.anchoredPosition;
        animator.SetFloat("speed", speed);
        jelatine = 1;
        level = 1;
        exp = 0;
        nextLvUp = 5;
    }
 
    void Update()
    {
        animator.SetBool("walk", true);
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition,
        newPosition, speed * Time.deltaTime);
        if (Vector2.Distance(rect.anchoredPosition, newPosition) < 0.1f)
        {
            animator.SetBool("walk", false);
            if (waitTime <= 0)
            {
                newPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                if(rect.anchoredPosition.x - newPosition.x > 0) {
                    img.transform.localScale = new Vector3(-1, 1, 1);
                }
                else {
                    img.transform.localScale = new Vector3(1, 1, 1);
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime * Random.Range(0, startWaitTime);
            }
        }

    }

    public void OnClick() {
        gameManager.setJelatine(gameManager.getJelatine() + this.jelatine);
        exp++;
        if(exp >= nextLvUp) {
            level++;
            //nextLvUp = //TODO: update number
            exp = 0;
        }
        Debug.Log("Clicked!");
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
