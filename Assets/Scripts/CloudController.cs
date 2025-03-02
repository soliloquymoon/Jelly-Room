using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloudController : MonoBehaviour
{
    private float speed; // Speed of cloud
    private Animator animator;
    private RectTransform rect;
    private const int MIN_Y = -150; // Minimum y position
    
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rect = this.GetComponent<RectTransform>();
        NewCloud();
    }

    private IEnumerator ResetCloud() {
        yield return new WaitForSeconds(3f);
        NewCloud();
        animator.Play("Cloud_Idle", -1, 0f);
    }

    // Set a new cloud
    private void NewCloud() {
        rect.anchoredPosition = new Vector2(0, Random.Range(MIN_Y, 0));
        speed = Random.Range(0.1f, 1);
        animator.SetFloat("speed", speed);
    }
}
