using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{

    public GameObject dialog;

    public GameObject Guide;

    public SetLayer SetLayer;

    private bool IsClose=false;

    public Tetris Tetris;
    // Start is called before the first frame update
    void Awake()
    {
        //ShowDialog(true);
        MapModel.IsPause = true;
        SetLayer.PlayEnd += PlayIsEnd;
        IsClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            CloseDialog();
            IsClose = true;
        }
    }
    // 显示提示框
    public void ShowDialog(bool isStart)
    {
        if (MapModel.CurrentLevel==1)
        {
            return;
        }
        int level = MapModel.CurrentLevel - 1;
        Debug.Log("Level:" + level);
        Text content = dialog.GetComponentInChildren<Text>();
        if (isStart)
        {

            content.text = MapModel.dialogList[level].StartMsg.content;
        }
        else
        {
            content.text = MapModel.dialogList[level].EndMsg.content;
        }
        Debug.Log("内容" + content.text);
        dialog.SetActive(true);
        Invoke("CloseDialog", 10f);
    }
    void PlayIsEnd()
    {
        ShowDialog(true);
    }
    void CloseDialog()
    {
        if (!IsClose)
        {
            dialog.SetActive(false);
            ShowGuida();
        }
    }

    void ShowGuida()
    {
        Guide.SetActive(true);
        Guide.GetComponent<GuideControl>().isEndGuide += IsEndGuide;
    }

    void IsEndGuide()
    {

        Tetris.SetStart();
        MapModel.IsPause = false;
    }
}
