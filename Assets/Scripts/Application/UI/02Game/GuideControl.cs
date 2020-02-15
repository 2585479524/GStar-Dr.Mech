using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideControl : MonoBehaviour
{
    public List<Image> Img_Guide;

    public delegate void IsEndGuide();
    public IsEndGuide isEndGuide;
    private int sum;
    //private float Timer=1.0f;
    // private bool CanGet=true;
    // Start is called before the first frame update
    void Start()
    {
        ShowGuide();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    /*void SetCanGet()
    {
        CanGet = true;
    }*/
    void ShowGuide()
    {
        for (int i = 0; i < 3; i++)
        {
            // String path = "UI/Guide/Img_Guide" + (ConstName.GuideNum[MapModel.CurrentLevel, i] + 1);
            // GameObject guide = Resources.Load<GameObject>(path) as GameObject;
            int index = ConstName.GuideNum[MapModel.CurrentLevel, i];
            Debug.Log(index);
            if (index != 0)
            {
                Image prefabInstance = Instantiate(Img_Guide[index - 1]);
                prefabInstance.transform.parent = transform;
                prefabInstance.transform.position = transform.position;
                prefabInstance.transform.localScale = new Vector3(1, 1, 1);
                prefabInstance.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Close();
                });
            }
            sum = transform.childCount - 1;
        }
    }
    void CloseGuide(int i)
    {
        transform.GetChild(i).gameObject.SetActive(false);
        Debug.Log("关闭" + i);
    }

    public void Close()
    {
        CloseGuide(sum);
        if (sum == 0)
        {
            gameObject.SetActive(false);
            isEndGuide();
        }
        else
        {
            sum--;
        }
    }
}
