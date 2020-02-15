using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Base_Grid
{

    #region 字段

    public bool isHaveBlock;
    public Block block;
    #endregion
}

public static class MapModel
{
    public static void InitMapModel()
    {
        base_Grids = new Base_Grid[ConstName.MapWidth, ConstName.MapHeight];
        AllBlocks = new List<Block>();
        SetBigBlock(ConstName.LandWidth, ConstName.LandHeight, ConstName.LandLenth);

       
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");

        Debug.Log("当前关卡" + CurrentLevel);

        PartsCount = new int[3];
        for (int i = 0; i < 3; i++)
        {
            PartsCount[i] = ConstName.LevelParts1TypeNum[CurrentLevel-1, i];
            Debug.Log(PartsCount[i]);
        }
        Block = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            Block[i] = Resources.Load<GameObject>("Tetris/Block" + i);
            if (Block[i] == null)
            {
                Debug.Log("未找到");
            }
        }
        //ShowHaveBlock();


        dialogList.Add(new Dialog(1, new Message("麦克", "在小恩娅回来前，我先练练手，不要生疏了。"), new Message("麦克", "等工作台修好了，机械医馆就可以重新开业了。")));

        dialogList.Add(new Dialog(2, new Message("恩娅", "麦克爷爷，零件我带来了，快去修理工作台吧！"), new Message("", "麦克爷爷，工作台转起来了，那我走啦，过几天再来找你玩。")));

        dialogList.Add(new Dialog(3, new Message("旅行者", "你好，是麦克医生么？我的零件出了点问题。"), new Message("旅行者", "哇喔，就像新的一样！太感谢了，麦克医生！这样我能继续旅行了。")));

        dialogList.Add(new Dialog(4, new Message("商人", "哦吼，我的医生朋友，请您医治我的右臂，我会付给你很多钱的。"), new Message("商人", "再见，麦克医生，我会在商会朋友面前，夸耀您高超的修理技巧的。")));

        dialogList.Add(new Dialog(5, new Message("斧头帮二当家", "麦克老东西，快帮帮我，不然我们斧头帮砸了你这破烂地方。"), new Message("斧头帮二当家", "麦克医生，我也不想总是受伤的。嗯…谢谢你。")));

        dialogList.Add(new Dialog(6, new Message("阿🐱", "麦克医生，求你修修阿猪，他吃东西的时候摔断了腿。"), new Message("阿🐖", "呼呼，麦克医生。阿猫，我们走，去吃点吃东西。")));
    }
    /// <summary>
    /// 构造函数
    /// </summary>                      
    #region 字段
    public static Base_Grid[,] base_Grids;
    public static List<Block> AllBlocks;


    public static List<Dialog> dialogList = new List<Dialog>();

    public static int[] PartsCount;

    public static bool IsAcce = false;

    public static bool IsPause = false;

    public static int CurrentLevel = 1;

    public static BigBlock CurrentShootBigBlock;  //记录当前发射方块

    public static List<Block> LandBlocks = new List<Block>();    //记录落地的方块

    public static int FirstDelenum = 0;

    //public static bool IsDelePart=false;

    public static GameObject[] Block;

    public static int[,] Shape = new int[3, 10] {
        { 1,1,1,1,1,1,0,0,0,0},
        { 1,0,0,0,0,0,0,0,0,0} ,
        { 1,0,0,0,0,0,0,0,0,0} };

    public static int[,] ShapeCloum = new int[10, 3] {
         {   1,1,1        }           ,
         {   0,0,1        }    ,
         {   1,1,1        }    ,
         {   0,0,1        }  ,
         {   0,0,1        }  ,
         {   0,0,1        }    ,
         {   0,0,0        }        ,
         {   0,0,0        }    ,
         {   0,0,0        }    ,
         {   0,0,0        }    ,
     };


    #endregion



    #region 属性
    #endregion


    public static void SetBlock(int pos_x, int pos_y, int[,] shape)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int cloum = 0; cloum < 10; cloum++)
            {

                if (shape[row, cloum] == 1)
                {
                    base_Grids[pos_x + cloum, pos_y - row].isHaveBlock = true;
                }

            }
        }

    }

    #region 方法
    public static void SetBigBlock(int pos_x, int pos_y, int lenth)
    {

        int count = 0;
        for (int i = pos_x; i < pos_x + lenth; i++)
        {
            for (int j = pos_y; j < pos_y + lenth; j++)
            {
                count++;
                base_Grids[i, j].isHaveBlock = true;
            }
        }

        //Debug.LogFormat("BigBlock Count ={0}", count);

        // SetBlock(pos_x, pos_y-1, MapModel.Shape);

        // SetBlock(pos_x, pos_y - 1, MapModel.ShapeCloum);
    }

    public static void ShowHaveBlock()
    {
        for (int i = 0; i < ConstName.MapWidth; i++)
        {
            for (int j = 0; j < ConstName.MapHeight; j++)
            {
                if (base_Grids[i, j].isHaveBlock)
                {
                    Debug.Log(i + " " + j);
                }
            }
        }
    }
    public static void Redata()
    {
        for (int i = 0; i < ConstName.MapWidth; i++)
        {
            for (int j = 0; j < ConstName.MapHeight; j++)
            {
                base_Grids[i, j].isHaveBlock = false;
                base_Grids[i, j].block = null;
            }
        }
        SetBigBlock(ConstName.LandWidth, ConstName.LandHeight, ConstName.LandLenth);

        for (int i = 0; i < AllBlocks.Count; i++)
        {
            AllBlocks[i].ChangeData();
        }
    }

    #endregion



    #region 外部接口



    // 改变list<Block>的方法

    // 各种通知View层的方法


    //返回一个X和Y都是整数的二维向量（四舍五入）
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }


    //当前关卡判赢条件
    public static bool GameWinOneCheck()
    {
        if (MapModel.CurrentLevel==1)
        {
            return false;
        }
        return (PartsCount[0] == 0 && PartsCount[1] == 0 && PartsCount[2] == 0);
    }

    public static bool GameOverCheck()
    {
        for (int x = ConstName.LandWidth + ConstName.LandLenth + ConstName.Border; x < ConstName.MapWidth; x++)
        {
            for (int y = ConstName.LandHeight - ConstName.Border; y < ConstName.LandHeight + ConstName.LandLenth + ConstName.Border; y++)
            {
                if (base_Grids[x, y].isHaveBlock)
                {
                    return true;
                }
            }
        }
        return false;
    }



    //左闭右开

    //检查某个方块是否在边界之内
    public static bool insideBorder(Postion pos)
    {




        //return    ((int)pos.x >=ConstName.LandWidth-ConstName.Border &&
        //             (int)pos.x < ConstName.LandWidth + ConstName.LandLenth+ ConstName.Border );



        bool result = false;
        //方块在陆地上下
        if ((pos.y < ConstName.LandHeight) || (pos.y > ConstName.LandHeight + ConstName.LandLenth))
        {


            result = ((int)pos.x >= ConstName.LandWidth - ConstName.Border &&
                    (int)pos.x < ConstName.LandWidth + ConstName.LandLenth + ConstName.Border);


            //result = ((int)pos.x >= 0 && (int)pos.x < ConstName.MapWidth &&
            //          (int)pos.y >= 0 && (int)pos.y < ConstName.MapHeight);

        }
        //方块在陆地中间
        else
        {

            result = ((pos.x >= ConstName.LandWidth - ConstName.Border &&
                        pos.x < ConstName.LandWidth)
                        ||
                       (pos.x >= ConstName.LandWidth + ConstName.LandLenth &&
                        pos.x < ConstName.LandWidth + ConstName.LandLenth + ConstName.Border));



            //result = ((int)pos.x >= 0 && (int)pos.x < ConstName.LandWidth) ||
            //         ((int)pos.x > ConstName.LandWidth + ConstName.LandLenth && (int)pos.x < ConstName.MapWidth);
        }
        return result;


        //return ((int)pos.x >= 0 &&
        //        (int)pos.x < w &&
        //        (int)pos.y >= 0);
    }

    /*public static void  PartDeTest(int x, int y)
    {
        int Index = (int)base_Grids[x, y].block.BlockType - 2;

        if (Index<=0)
        {
            return;
        }
        int t = PartsCount[Index];
        t--;
        t = ((t == -1) ? 0 : t);
        PartsCount[Index] = t;

        Debug.Log(Index + ":" + PartsCount[Index]);
        //统计零件数量
    }*/

    //删除某一被堆满方块的行
    public static void deleteRow(int y)
    {
        FirstDelenum++;
        // Debug.LogFormat("deleteRow = {0}", y);
        int x = 0;
        for (; x < ConstName.MapWidth; ++x)
        {


            //统计零件数量
            if (base_Grids[x, y].isHaveBlock)
            {
                //统计零件数量
                // PartDeTest(x, y);
                // Debug.Log("行测试" );
                base_Grids[x, y].isHaveBlock = false;

                if (base_Grids[x, y].block)
                {
                    base_Grids[x, y].block.Delete(DeleteType.Across);
                }
            }

        }



        Game.Instance.Sound.PlayEffect("delete", false);


    }
    //当某一行被删除后，便让下一行的所有方块移到这一行上  y为被删除的行
    public static void decreaseRow(int y, int count)
    {
        // Debug.LogFormat("decreaseRow={0} count={1}", y,count);


        if (y < ConstName.LandHeight) //从下往上落
        {
            for (int x = 0; x < ConstName.MapWidth; ++x)
            {

                if (base_Grids[x, y - count].isHaveBlock)
                {

                    // Debug.LogFormat("base_Grids[x, y-count] =[{0}, {1}]", x, y-count);

                    base_Grids[x, y] = base_Grids[x, y - count];
                    base_Grids[x, y - count].isHaveBlock = false;

                    if (base_Grids[x, y - count].block)
                    {

                        base_Grids[x, y - count].block.MoveUp(count);


                    }

                }

            }
        }
        else  //从上往下落
        {
            for (int x = 0; x < ConstName.MapWidth; ++x)
            {
                if (base_Grids[x, y + count].isHaveBlock)
                {
                    base_Grids[x, y] = base_Grids[x, y + count];
                    base_Grids[x, y + count].isHaveBlock = false;
                    if (base_Grids[x, y + count].block)
                    {
                        base_Grids[x, y + count].block.MoveDown(count);
                    }
                }

            }
        }
    }
    //将所有在删除行之上（下）的所有方块都往上（下）移动count行
    public static void decreaseRowsAbove(int y, int count)
    {
        //  Debug.LogFormat("decreaseRowsAbove  y={0}  count={1}", y,count);
        if (y < ConstName.LandHeight) //从下往上落
        {
            for (int i = y; i > 0 + count; --i)
            {
                decreaseRow(i, count);
            }
        }
        else  //从上往下落
        {
            for (int i = y; i < ConstName.MapHeight - 1 + count - 3; ++i)
            {
                decreaseRow(i, count);
            }
        }



        //for (int i = y; i < h; ++i)
        //    decreaseRow(i);
    }
    //判断方块所在行是否被填满方块：
    public static bool isRowFull(int y)
    {

        //方块在陆地上下
        if ((y < ConstName.LandHeight) || (y >= ConstName.LandHeight + ConstName.LandLenth))
        {
            for (int x = ConstName.LandWidth; x < ConstName.LandWidth + ConstName.LandLenth; ++x)
            {
                if (!(base_Grids[x, y].isHaveBlock))
                {
                    return false;
                }
            }

            Debug.LogFormat("isRowFu = {0} ===true", y);

            return true;

        }
        //方块在陆地中间
        else
        {
            return false;
        }


    }
    //删除所有被填满的行上的方块
    public static void deleteFullRows()
    {
        Debug.LogFormat("deleteFullRow");

        int firstDeletedRow = -1;
        int moveRows = 0;


        for (int y = 0; y < ConstName.MapHeight; y++)
        {


            if (isRowFull(y))
            {
                firstDeletedRow = y;
                deleteRow(y);
                moveRows++;
                y++;
                while (isRowFull(y))
                {
                    deleteRow(y);
                    moveRows++;
                    y++;
                }

                // Debug.LogFormat("359  y={0}  moveRows={1}  firstDeletedRow = {2}  moveRows={3} ", y,moveRows, firstDeletedRow, moveRows);
            }

            if ((y - 1) < ConstName.LandHeight)
            {
                if (moveRows != 0)
                {

                    firstDeletedRow = y - 1;

                    // Debug.LogFormat("firstDeletedRow=【{0}】", firstDeletedRow);
                }
            }

            if (moveRows != 0)
            {
                //Debug.LogFormat("firstDeletedRow=【{0}】 moveRows=[{1}]", firstDeletedRow, moveRows);
                decreaseRowsAbove(firstDeletedRow, moveRows);
            }

            y -= moveRows;

            firstDeletedRow = -1;
            moveRows = 0;

            //if (isRowFull(y))
            //{



            //    deleteRow(y);
            //    decreaseRowsAbove(y, 1);
            //    --y;
            //}
        }




        //for (int x = 0; x < w; ++x)
        //    if (grid[x, y] == null)
        //        return false;
        //return true;
    }


    //删除某一被堆满方块的列
    public static void deleteColumn(int x)
    {

        FirstDelenum++;
        for (int y = 0; y < ConstName.MapHeight; ++y)
        {
            if (base_Grids[x, y].isHaveBlock)
            {
                // PartDeTest(x, y);



                base_Grids[x, y].isHaveBlock = false;
                if (base_Grids[x, y].block)
                    base_Grids[x, y].block.Delete(DeleteType.Vertical);
            }
        }
        Game.Instance.Sound.PlayEffect("delete", false);
    }
    //当某一列被删除后，便让下一列的所有方块移到这一列上
    public static void decreaseColumn(int x, int count)
    {
        if (x < ConstName.LandWidth) //从左往右落
        {
            for (int y = 0; y < ConstName.MapHeight; ++y)
            {

                if (base_Grids[x - count, y].isHaveBlock)
                {
                    base_Grids[x, y] = base_Grids[x - count, y];
                    base_Grids[x - count, y].isHaveBlock = false;
                    if (base_Grids[x - count, y].block)
                        base_Grids[x - count, y].block.MoveRight(count);
                }

            }
        }
        else  //从右往左落
        {
            for (int y = 0; y < ConstName.MapHeight; ++y)
            {
                if (base_Grids[x + count, y].isHaveBlock)
                {
                    base_Grids[x, y] = base_Grids[x + count, y];
                    base_Grids[x + count, y].isHaveBlock = false;
                    if (base_Grids[x + count, y].block)
                        base_Grids[x + count, y].block.MoveLift(count);
                }

            }
        }


        //for (int x = 0; x < w; ++x)
        //{
        //    if (grid[x, y] != null)
        //    {
        //        // Move one towards bottom
        //        grid[x, y - 1] = grid[x, y];
        //        grid[x, y] = null;

        //        // Update Block position
        //        grid[x, y - 1].position += new Vector3(0, -1, 0);
        //    }
        //}
    }
    //将所有在删除列之左（右）的所有方块都往左（右）移动一列
    public static void decreaseColumnAbove(int x, int count)
    {

        if (x < ConstName.LandWidth) //从左往右落
        {
            for (int i = x; i > 0 + count; --i)
            {
                decreaseColumn(i, count);
            }
        }
        else  //从右往左落
        {
            for (int i = x; i < ConstName.MapWidth - 1 + count - 3; ++i)
            {
                decreaseColumn(i, count);
            }
        }




        //for (int i = y; i < h; ++i)
        //    decreaseRow(i);
    }
    //判断方块所在列是否被填满方块：
    public static bool isColumnFull(int x)
    {

        //方块在陆地左右
        if ((x < ConstName.LandWidth) || (x >= ConstName.LandWidth + ConstName.LandLenth))
        {
            for (int y = ConstName.LandHeight; y < ConstName.LandHeight + ConstName.LandLenth; ++y)
            {
                if (!(base_Grids[x, y].isHaveBlock))
                {
                    return false;
                }
            }
            return true;

        }
        //方块在陆地中间
        else
        {
            return false;
        }

        //for (int x = 0; x < w; ++x)
        //    if (grid[x, y] == null)
        //        return false;
        //return true;
    }
    //删除所有被填满的列上的方块
    public static void deleteFullColumn()
    {

        int firstDeletedRow = -1;
        int moveRows = 0;


        for (int x = 0; x < ConstName.MapWidth; x++)
        {

            if (isColumnFull(x))
            {
                firstDeletedRow = x;
                deleteColumn(x);
                moveRows++;
                x++;
                while (isColumnFull(x))
                {
                    deleteColumn(x);
                    moveRows++;
                    x++;
                }

                Debug.LogFormat("545  x={0}  moveRows={1}  firstDeletedRow = {2}  moveRows={3} ", x, moveRows, firstDeletedRow, moveRows);
            }


            if ((x - 1) < ConstName.LandWidth)
            {
                if (moveRows != 0)
                {

                    firstDeletedRow = x - 1;

                    // Debug.LogFormat("firstDeletedRow=【{0}】", firstDeletedRow);
                }
            }


            if (moveRows != 0)
            {
                //decreaseRowsAbove(firstDeletedRow, moveRows);
                decreaseColumnAbove(firstDeletedRow, moveRows);
            }

            x -= moveRows;

            firstDeletedRow = -1;
            moveRows = 0;


        }

        //for (int x = 0; x < ConstName.MapWidth; x++)
        //{
        //    if (isColumnFull(x))
        //    {
        //        deleteColumn(x);
        //        decreaseColumnAbove(x);
        //        --x;
        //    }
        //}
    }


    #endregion


}
