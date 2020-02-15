using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    private Touch touch;

    private int step;

    private Vector3 StartPos;

    private Vector3 EndPos;

    private int MoValue = 0;

    public BigBlock BigBlock;
    /*private float TimerRo = 0.07f;
    private float Timer = 0;*/
    private bool Move = false;

    private bool CanRo = false;

    public float velocityY;
    public float velocityX;
    private bool IsArr
    {
        get
        {
            return step == MoValue;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MapModel.IsPause)
        {
            return;
        }
        if (BigBlock == null)
        {
            return;
        }
        int touchNum = Input.touchCount;

        if (touchNum == 0)
        {
            MoValue = 0;
            step = 0;
            return;
        }
        touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            StartPos = Camera.main.ScreenToWorldPoint(touch.position);
            CanRo = true;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            EndPos = Camera.main.ScreenToWorldPoint(touch.position);
            step = (int)(EndPos.x - StartPos.x);
            CanRo = false;
        }

        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        float tempY = touchDeltaPosition.y;

        float tempX = touchDeltaPosition.x;


        velocityX = tempX / Time.deltaTime;

        //速度
        velocityY = tempY / Time.deltaTime;


        if (Mathf.Abs(velocityX)<1000f&&velocityY > 1500f)
        {
            GetComponent<Tetris>().Accelerate();
        }

        if (touch.phase == TouchPhase.Ended)
        {
            if (CanRo)
            {
                BigBlock.Ratate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (IsArr)
        {
            return;
        }


        if (step > MoValue)
        {
            MoValue++;
            BigBlock.Right();
        }
        else
        {
            MoValue--;
            BigBlock.Left();
        }

    }
}
