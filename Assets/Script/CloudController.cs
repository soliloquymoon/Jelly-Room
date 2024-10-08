using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloudController : MonoBehaviour
{
    float speed;
    private float waitTime;
    private Animator animator;
    private RectTransform rect;
    private int minY = -150;
    
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rect = this.GetComponent<RectTransform>();
        NewCloud();
    }

    IEnumerator ResetCloud() {
        yield return new WaitForSeconds(3f);
        NewCloud();
        animator.Play("Cloud_Idle", -1, 0f);
    }

    void NewCloud() {
        Debug.Log("new cloud");
        rect.anchoredPosition = new Vector2(0, Random.Range(minY, 0));
        speed = Random.Range(0.1f, 1);
        animator.SetFloat("speed", speed);
        Debug.Log(speed);
    }

}
