using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // 失败面板
    public GameObject failed;

    // 提示框面板

    [HideInInspector]
    public GameObject dialog;

    // 进入动画
    public List<Animator> animator;
    void Awake()
    {
        
    }
    private void Start()
    {
       // ShowDialog();
    }

    private void Update()
    {
        animator[0].SetBool("isShow", true);
        Invoke("ShowLight", 0.2f);
        Invoke("ShowBackpack", 1f);
    }

    // 判输，打开面板
    public void Judge()
    {
        failed.SetActive(true);
    }
    // 确定后，关闭并退回level：1
    public void Exit()
    {
        failed.SetActive(false);
        Game.Instance.LoadScene(1);
    }

   


    void ShowLight()
    {
        animator[1].gameObject.SetActive(true);
        animator[1].SetBool("isShow", true);
    }

    void ShowBackpack()
    {
        animator[2].SetBool("isShow", true);
    }
    public void Continue()
    {
        Game.Instance.LoadScene(2);
    }
}