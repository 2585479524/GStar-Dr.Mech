using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    land,
    block,
    parts1,
    parts2,
    parts3
}
public enum BlockStatus
{
    Wait,
    Move,
    Clean,
    Fixed,
}
public enum DeleteType
{
    Across,
    Vertical,
}
public class Block : MonoBehaviour
{
    #region 字段
    public BlockType BlockType;
    public BlockStatus blockStatus;
    public delegate void MoveTestReturn(bool CanMove);
    public MoveTestReturn TestComplete;

    public GameObject broken;



    public bool CanRight;
    public bool Canleft;

    private Vector3 TargetPos;



    bool IsArr = true;


    private Transform Child;
    #endregion
    private void Awake()
    {
        Child = transform.GetChild(2);
    }
    #region 属性
    #endregion
    private void Update()
    {
        //缓慢移动
        if (!IsArr)
        {
            transform.position = Vector3.Lerp(transform.position, Child.position, Time.deltaTime * ConstName.BlockDeleSpeed);
            Vector3 v = Child.position - transform.position;
            if (v.sqrMagnitude < 0.1f)
            {
                transform.position = Child.position;
                Child.SetParent(transform);
                IsArr = true;
            }
        }
    }

    public void TestCanMove()
    {
        if (blockStatus == BlockStatus.Move)
        {
            Postion postion = GetIndex();
            postion.y += 1;
            bool IsCanMove = !IsHaveBlock(postion);
            if (!IsCanMove)
            {
                Debug.Log("停止");
                
                blockStatus = BlockStatus.Fixed;
            }
            /*if (postion.y >= ConstName.MapHeight - 1)
            {
                transform.parent.GetComponent<BigBlock>().Faild();
            }*/
            TestComplete(IsCanMove);

           /* if (postion.y == 26)
            {
                //transform.parent.position;
            }*/
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    public void TestLeftAndRight()
    {
        Postion postion = GetIndex();
        postion.x += 1;
        CanRight = !IsHaveBlock(postion);

        postion.x -= 2;
        Canleft = !IsHaveBlock(postion);
    }
    public bool IsHaveBlock(Postion postion)
    {
        return MapModel.base_Grids[postion.x, postion.y].isHaveBlock;
    }
    #region 方法
    public Postion GetIndex()
    {
        Postion postion;
        postion.x = (int)transform.position.x;
        postion.y = (int)transform.position.y;
        return postion;
    }
    public void ChangeData()
    {
        Postion postion = GetIndex();
        MapModel.base_Grids[postion.x, postion.y].isHaveBlock = true;
        MapModel.base_Grids[postion.x, postion.y].block = this;
    }
    public void CleanData()
    {
        Postion postion = GetIndex();
        MapModel.base_Grids[postion.x, postion.y].isHaveBlock = false;
        MapModel.base_Grids[postion.x, postion.y].block = null;
    }


    void BrokenPlay()
    {

        GameObject te = Instantiate(broken, new Vector3(this.GetIndex().x, this.GetIndex().y + 0.5f), Quaternion.identity) as GameObject;
        te.GetComponent<ParticleSystem>().Play();

    }
    /// <summary>
    /// 消除入口
    /// </summary>
    public void Delete(DeleteType deleteType )
    {
        int Blocktype = (int)BlockType;
        int DeleteType = (int)deleteType;
        if (Blocktype ==4|| (DeleteType+2==Blocktype))
        {
            transform.GetChild(0).GetComponent<Part>().Trigger();

            if (MapModel.PartsCount[Blocktype - 2]>0)
            {
                MapModel.PartsCount[Blocktype - 2]--;
            }
        }
        //transform.GetChild(0).GetComponent<Part>()
        Debug.Log("隐藏方块");
        if (MapModel.AllBlocks.Contains(this))
        {

            BrokenPlay();

            MapModel.AllBlocks.Remove(this);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    ///
    public void MoveUp(int step=1)
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y + step, 0);
        Child.position = TargetPos;
        Child.SetParent(transform.parent);
        IsArr = false;
    }

    public void MoveDown(int step=1)
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y - step, 0);
        Child.position = TargetPos;
        Child.SetParent(transform.parent);
        IsArr = false;
    }

    public void MoveLift(int step=1)
    {
        TargetPos = new Vector3(transform.position.x - step, transform.position.y, 0);
        Child.position = TargetPos;
        Child.SetParent(transform.parent);
        IsArr = false;
    }
    public void MoveRight(int step=1)
    {
        TargetPos = new Vector3(transform.position.x + step, transform.position.y, 0);
        Child.position = TargetPos;
        Child.SetParent(transform.parent);
        IsArr = false;
    }


    public void SetType(bool IsPatrs)
    {
        if (IsPatrs)
        {
            
            int ra = Random.Range(4, 7);
            BlockType = (BlockType)(ra - 2);
            GameObject bx = MapModel.Block[ra];
            GetComponent<SpriteRenderer>().sprite = bx.GetComponent<SpriteRenderer>().sprite;

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bx.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = bx.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            BlockType = BlockType.block;
            int rb = Random.Range(0, 4);
            GameObject bx = MapModel.Block[rb];
            GetComponent<SpriteRenderer>().sprite = bx.GetComponent<SpriteRenderer>().sprite;
        }
        //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;




    }


    public void SetType(bool IsPatrs, int level)
    {



        if (IsPatrs && level > 1)
        {

            int ra = 3;

            switch (level)
            {
                case 2:
                case 3: ra = 6; break;
                case 4: ra = (Random.Range(4, 6) == 4) ? 4 : 6; break;
                case 5: ra = Random.Range(5, 7); break;
                case 6: ra = Random.Range(4, 7); break;


                default:
                    break;
            }

            BlockType = (BlockType)(ra - 2);
            GameObject bx = MapModel.Block[ra];
            GetComponent<SpriteRenderer>().sprite = bx.GetComponent<SpriteRenderer>().sprite;

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bx.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = bx.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            BlockType = BlockType.block;
            int rb = Random.Range(0, 4);
            GameObject bx = MapModel.Block[rb];
            GetComponent<SpriteRenderer>().sprite = bx.GetComponent<SpriteRenderer>().sprite;
        }
        //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
    }

    #endregion
}