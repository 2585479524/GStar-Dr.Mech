using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimControl : MonoBehaviour
{
    public Image img_bg;
    private bool isClick = false;
    private void Awake()
    {

        Debug.Log(MapModel.CurrentLevel);
        if (MapModel.CurrentLevel!=1)
        {
            transform.parent. gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isClick)
        {
            gameObject.GetComponent<Animator>().SetTrigger("isClick");
        }
    }
    void CloseAnim()
    {
        gameObject.SetActive(false);
        img_bg.gameObject.SetActive(false);
    }

    void ListenClick()
    {
        isClick = true;
    }
}

