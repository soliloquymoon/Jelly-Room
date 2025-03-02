using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private string[] messages = new string[]{
        "Touch your jellies to collect jelatine!",
        "Jellies are squishy and sweet!",
        "Level up your jellies to earn more gold",
        "Unlock jellies and complete your collection!",
        "Sell your jellies when they reach level 5 for some shiny gold!",
        "Each jelly has its own flavor — some give more jelatine than others!",
        "Want more jelatine? Upgrade your productivity level!",
        "Increase the capacity! You can now keep more jellies in your room.",
        "Tap to touch, level to grow — your jellies are waiting!",
        "Upgrade your jellies to unlock even more squishy surprises!",
        "The higher your jelly's level, the more jelatine you’ll get!",
        "Need gold? Sell your jellies for a sweet reward!",
        "Each touch brings you closer to upgrading your jellies!",
        "Unlock new jellies by purchasing them with your gold and jelatine!",
        "Jellies grow bigger as you level them up!",
        "More jellies, more Jelatine! Expand your collection by leveling up your room!",
        "The better your productivity level, the faster you fill your jelatine jar!",
        "Gold and jelatine are the keys to unlocking your jelly empire!",
        "Your jellies are waiting for their next upgrade — tap away!"
    };

    public Slider loadingBar;
    public Text message;

    void Start()
    {
        Text message = GameObject.Find("Message").GetComponent<Text>();
        Slider loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();
        // Randomly set the loading message
        message.text = messages[Random.Range(0, messages.Length)];
        StartCoroutine(UpdateLoadingBar());
    }

    private IEnumerator UpdateLoadingBar()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("GameScene");
        loadScene.allowSceneActivation = false;

        // Update the fake loading bar
        loadingBar.value = 0f;
        float elapsedTime = 0f;
        float duration = 3f;

        while (elapsedTime < duration)
        {
            loadingBar.value = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loadingBar.value = 1f;

        loadScene.allowSceneActivation = true;
    }
}