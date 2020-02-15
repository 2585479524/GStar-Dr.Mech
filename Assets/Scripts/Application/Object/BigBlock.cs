using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBlock : MonoBehaviour
{
    public delegate void Stop();

    public Stop stop;

    public delegate void IsUp();
    public IsUp isUp;
    public Block[] blocks;

    private Transform follewPoint;
    public List<Vector2> landingPosition = new List<Vector2>();    //落地预判位置
    //public bool IsWait = false;
    private bool IsMove = true;
    private bool canMove = true;
    //private bool Bigger = false;

    //private bool SetPos = false;
    //private Vector3 TargetPos;

    private bool CanRight;
    private bool CanLeft;
    private Vector3 RoScale=new Vector3(1f,1f,1f);
    public Vector3 WaitScale=new Vector3(0.7f,0.7f,1f);


    void Awake()
    {
        blocks = GetComponentsInChildren<Block>();

        RoScale = transform.localScale;

        transform.localScale = transform.localScale/3f;
        //随机判断是否有零件
        int ta = Random.Range(0, 100);
        if (ta <= ConstName.CreatPartsProbabity)
        {
            //有零件

            int ra = Random.Range(0, blocks.Length);
            for (int i = 0; i < blocks.Length; i++)
            {
                    blocks[i].SetType(i==ra,MapModel.CurrentLevel);
            }
        } 
        else
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].SetType(false, MapModel.CurrentLevel);
            }
        }
        foreach (Block item in blocks)
        {
            item.TestComplete += MoveTestReturn;
        }
    }

    
    


    private void Update()
    {
        /*if (IsWait)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            if (follewPoint)
            {
                transform.position = follewPoint.position;
            }  
        }*/

        /*if (Bigger)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, RoScale, Time.deltaTime * 2);
            if ((transform.localScale- RoScale).sqrMagnitude<0.05f)
            {
                transform.localScale = RoScale;
                Bigger = false;
            }
        }*/

        /*if (SetPos)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 3);
            if ((transform.position - TargetPos).sqrMagnitude < 0.5f)
            {
                transform.position = TargetPos;
                SetPos = false;
            }
        }*/
    }

    /// <summary>
    /// 向指定位置修正
    /// </summary>
    public void SetCorrectPos()
    {
        Debug.Log("修正");
        //修正位置
        Vector3 po = transform.position;
        po.x = (int)po.x - 0.5f;
        po.y = (int)po.y + 2.5f;
       // TargetPos = po;
       // SetPos = true;
    }

    /// <summary>
    /// 变大
    /// </summary>
    /*public void BeBigger()
    {
        Bigger = true;
    }*/

    /// <summary>
    /// 等待函数
    /// </summary>
    /// <param name="follew"></param>
    /*public void Wait()
    {
        //follewPoint = follew;
        //IsWait = true;
        //transform.localScale = WaitScale;

        Bigger = true;
    }*/


    /// <summary>
    /// 通知移动，开始测试能否移动
    /// </summary>
    public void MoveMes()
    {
        canMove = true;
        if (IsMove)
        {

            if (transform.position.y>=26)
            {
               
                isUp();
                transform.position = new Vector3(9.5f, 4.5f,0);
            }
            foreach (Block item in blocks)
            {
                item.TestCanMove();
            }


            if (canMove)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                TestLeftAndRight();
            }
            else
            {
                IsMove = false;
                foreach (Block item in blocks)
                {
                    item.TestComplete += MoveTestReturn;
                    item.ChangeData();
                    item.blockStatus = BlockStatus.Fixed;
                    MapModel.AllBlocks.Add(item);
                }
                
                Transform Land = GameObject.Find("land").transform;
                for (int i = 0; i < blocks.Length; i++)
                {
                    blocks[i].transform.SetParent(Land);
                }
                
                stop();

                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// 测试能否左右移动
    /// </summary>
    private void TestLeftAndRight()
    {
        foreach (Block item in blocks)
        {
            item.TestLeftAndRight();
        }
        int i;
        for (i = 0; i < blocks.Length; i++)
        {
            if (!blocks[i].CanRight)
            {
                CanRight = false;
                break;
            }
        }
        if (i == blocks.Length)
        {
            CanRight = true;
        }
        for (i = 0; i < blocks.Length; i++)
        {
            if (!blocks[i].Canleft)
            {
                CanLeft = false;
                break;
            }
        }
        if (i == blocks.Length)
        {
            CanLeft = true;
        }
    }
    /// <summary>
    /// 测试结果
    /// </summary>
    /// <param name="Move"></param>
    public void MoveTestReturn(bool Move)
    {
        if (Move == false)
        {
            canMove = false;
        }
    }

    public void Faild()
    {
        MapModel.IsPause=true;
        Time.timeScale = 0;
    }

    /// <summary>
    /// 发射
    /// </summary>
    public void Shoot()
    {
        //IsWait = false;

        transform.localScale = RoScale;
        SetBlockStatus(BlockStatus.Move);
        SetCorrectPos();
        
        //BeBigger();
    }
    public void SetBlockStatus(BlockStatus blockStatus)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].blockStatus = blockStatus;
        }
    }
    public void Left()
    {
        if (!isValidGridPos(-1))
        {
            return;
        }

        if (MapModel.IsAcce)
        {
            return;
        }
        if (CanLeft)
        {
            transform.position += new Vector3(-1, 0, 0);
            Game.Instance.Sound.PlayEffect("Move_Sound", false);
        }
        TestLeftAndRight();


       // GetBeamPosition();

    }


    public void Right()
    {

        if (!isValidGridPos(1))
        {
            return;
        }
        if (MapModel.IsAcce)
        {
            return;
        }

        if (CanRight)
        {
            transform.position += new Vector3(+1, 0, 0);
            Debug.Log("播放声音右");
            Game.Instance.Sound.PlayEffect("Move_Sound", false);
        }
        TestLeftAndRight();
        /*bool canMove = true;
        foreach (Transform child in transform)
        {
            Vector2 v = MapModel.roundVec2(child.position + new Vector3(1, 0, 0));

            if (!MapModel.insideBorder(v) || MapModel.base_Grids[(int)v.x, (int)v.y].isHaveBlock)
            {
                canMove = false;
            }

        }
        if (canMove)
        {
            transform.position += new Vector3(1, 0, 0);
        }*/
    }


    public void Ratate()
    {  
        transform.Rotate(0, 0, -90);
        // See if valid
        if (isValidGridPos())
            return;
        // It's valid. Update grid.
        // updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);


        Game.Instance.Sound.PlayEffect("rotate", false);


    }









    /// <summary>
    /// ///
    /// </summary>

    public struct BeamEffect
    {
        public int postion_x;
        public int length;
    };


    BeamEffect BeamPostion = new BeamEffect();

    public BeamEffect GetBeamPosition()
    {


        List<Postion> vlist = new List<Postion>();

        foreach (var item in blocks)
        {
            vlist.Add(item.GetIndex());
            //   Debug.LogFormat("v.x={0} ========= ", v.x);

        }



        Postion xMax = vlist[0];
        Postion xMin = vlist[0];
        for (int i = 0; i < vlist.Count; i++)//这里遍历数组
        {
            if (xMax.x < vlist[i].x)//判断每个数大小
            {
                xMax = vlist[i];//最后这里等于最大值
            }

            if (xMax.x > vlist[i].x)
            {
                xMin = vlist[i];
            }

        }



        // list.Sort();


        BeamPostion.postion_x = (int)xMin.x;
        BeamPostion.length = (int)(xMax.x - xMin.x) + 1;




        return BeamPostion;
    }


    public bool IsSelfBlock(Postion v)
    {



        foreach (Block item in blocks)
        {

            if (item.GetIndex().x == v.x && item.GetIndex().y == v.y)
            {
                return true;
            }




        }


        return false;
    }



    //int countd = 0;

    public bool SetLandBlocks()
    {

        if (MapModel.LandBlocks != null && MapModel.LandBlocks.Count > 0)
        {

            MapModel.LandBlocks.Clear();

        }

        List<Postion> tmp = new List<Postion>();
        foreach (Block child in blocks)
        {
            Postion v;

            v.x = child.GetIndex().x;
            v.y = child.GetIndex().y + 1;

            tmp.Add(v);
        }

        //  countd++;


        foreach (Block child in blocks)
        {
            Postion v;

            v.x = child.GetIndex().x;
            v.y = child.GetIndex().y + 1;


            if (MapModel.base_Grids[(int)v.x, (int)v.y].isHaveBlock)               //该位置有方块
            {

                if (MapModel.base_Grids[(int)v.x, (int)v.y].block != null &&
                       !IsSelfBlock(v))                                    //       MapModel.base_Grids[(int)v.x, (int)v.y].block.transform != transform) //前面是其他方块
                {
                    MapModel.LandBlocks.Add(child);
                }
                else if (MapModel.base_Grids[(int)v.x, (int)v.y].block == null)  //前面是陆地
                {
                    MapModel.LandBlocks.Add(child);
                }

            }

        }

        return (MapModel.LandBlocks.Count != 0) ? true : false;
    }


    public bool isValidGridPos(int x = 0)
    {

        // int count = 0;
        //foreach (Transform child in transform)
        foreach (Block child in blocks)
        {
            // count++;
            Postion v;

            v.x = child.GetIndex().x + x;
            v.y = child.GetIndex().y;

            // Debug.Log(v);

            // Not inside Border?
            if (!MapModel.insideBorder(v))
            {
                //  Debug.LogFormat("if (!MapModel.insideBorder(v) 【{0}】  ccount[{1}] =FALSE", v, count);
                return false;
            }

        }

        // Debug.LogFormat("isValidGridPos()=========True");

        return true;
    }



}