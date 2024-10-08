using UnityEngine;
using UnityEngine.UI;

public class JellyController : MonoBehaviour
{
    public Image img;
    public float speed;
    public float startWaitTime;
    public GoodsManager goodsManager;
    private float waitTime;
    private int minX = -400, maxX = 400;
    private int minY = -90, maxY = 90;
    private Animator animator;
    private RectTransform rect;
    private Vector2 move;
    private int jelatine;
    
    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        waitTime = startWaitTime;
        animator = this.GetComponent<Animator>();
        move = rect.anchoredPosition;
        animator.SetFloat("speed", speed);
        jelatine = 1;
    }
 
    void Update()
    {
        animator.SetBool("walk", true);
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition,
        move, speed * Time.deltaTime);
        if (Vector2.Distance(rect.anchoredPosition, move) < 0.1f)
        {
            animator.SetBool("walk", false);
            if (waitTime <= 0)
            {
                move = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                if(rect.anchoredPosition.x - move.x > 0) {
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
        goodsManager.setJelatine(goodsManager.getJelatine() + this.jelatine);
        Debug.Log("Clicked!");
    }

}
