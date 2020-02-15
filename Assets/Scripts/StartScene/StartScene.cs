using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject blank1;
    public GameObject title;
    public GameObject blank2;
    public GameObject sign;
    public Sprite signSprite;
    public GameObject storyButton;
    public GameObject endlessButton;
    public GameObject newGameButton;
    public GameObject continueButton;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void ClickPlay()
    {
        animator.SetBool("play", true);
        sign.GetComponent<Button>().enabled = false;
        sign.GetComponent<Image>().sprite = signSprite;
        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        for (float timer = 0; timer < 2.2f; timer += Time.deltaTime)
        {
            yield return null;
        }
        animator.SetBool("play",false);
        blank1.GetComponent<Image>().enabled = false;
        sign.SetActive(false);
        title.SetActive(false);
        blank2.SetActive(true);
    }

    public void StoryMode() {
        storyButton.SetActive(false);
        endlessButton.SetActive(false);
        newGameButton.SetActive(true);
        continueButton.SetActive(true);
    }

    public void NewGame()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 1)
        {
            // 播放入场动画，新手引导
        }
        else
        {
            // 不播放
        }
        Game.Instance.LoadScene(2);
    }
    public void EndlessMode()
    {
        Game.Instance.LoadScene(3);
    }
}
