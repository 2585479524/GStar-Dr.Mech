using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ConstName
{
    /// <summary>
    /// 退出场景
    /// </summary>
    public const string E_ExitScene = "E_ExitScene";
    /// <summary>
    /// 进入场景
    /// </summary>
    public const string E_EnterScene = "E_EnterScene";
    /// <summary>
    /// 开始命令
    /// </summary>
    public const string E_StartUp = "E_StartUp";
    /// <summary>
    /// 发射方块
    /// </summary>
    public const string E_ShootBlock = "E_ShootBlock";
    /// <summary>
    /// 旋转所有的方块
    /// </summary>
    public const string E_RotateAllBlock = "E_RotateAllBlock";
    /// <summary>
    /// 旋转方块
    /// </summary>
    public const string E_RotateBlock = "E_RotateBlock";
    /// <summary>
    /// 生成方块
    /// </summary>
    public const string E_CreatBlock = "E_CreatBlock";
    /// <summary>
    /// 消除方块
    /// </summary>
    public const string E_EliminataBlock = "E_EliminataBlock";
    /// <summary>
    /// 对齐位置
    /// </summary>
    public const string E_AlignedPostion = "E_AlignedPostion";
    /// <summary>
    /// 暂停游戏
    /// </summary>
    public const string E_PauseGame = "E_PauseGame";
    /// <summary>
    /// 继续游戏
    /// </summary>
    public const string E_ContinueGame = "E_PauseGame";

    #region 目录


    //各个关卡需要收集零件数量
    public static int[,] LevelParts1TypeNum = new int[7,3] { 
        { 0, 0, 0},
        { 0, 0, 3 },
        { 0, 0, 6 } , 
        { 5, 0, 3 }, 
        { 0, 4, 5 },
        { 4, 4, 4 },
        { 4, 4, 4}};
    //public const int LevelParts1TypeNum = 3;
    public const int LevelCollectingParts1One = 1;
    public const int LevelCollectingParts2One = 0;
    public const int LevelCollectingParts3One = 0;

    public const int FirstVictoryNum=2;


    //边界范围
    public const int Border = 4;


    public const int MapWidth = 20;  //地图宽
    public const int MapHeight = 29; //地图高

    public const float TriggerUpSpeed = 1200f;

    public const int LandWidth = 7;  //大方块位置  x
    public const int LandHeight = 15;//大方块位置  y
    public const int LandLenth = 5;  //大方块长度 
    
    public static float MoveDeltaTime=1.5f;
    public static float AcceDeltaTime = 0f;

    public static float RotateDeltaTime = 3f;

    public static float ReDataDeltaTime = 0.2f;

    public static float BlockDeleSpeed = 6f;


    // 新手引导数量
    public static int[,] GuideNum = new int[6, 3]
    {
        {0, 0, 0},
        {3, 2, 1},
        {5, 4, 0},
        {7, 6, 0},
        {8, 0, 0},
        {9, 0, 0}
    };

    /// <summary>
    /// 生成零件的概率
    /// 说明：范围1~100;
    /// </summary>
    public const int CreatPartsProbabity=80;
    #endregion


    public const string V_Start = "V_Start";
    #region Models
    public const string M_Map = "M_MapModel";
    #endregion
    public const string M_Block = "M_Block";
    public const string M_Grid = "M_Grid";
    public const string M_Land = "M_Land";





    public static float[,] characterScale = new float[7, 2]
        {
           {-0.4f  ,-0.4f },
           {-0.5f  ,-0.4f },
           {-0.5f  ,-0.4f },
           {-1.28f ,-0.9f },
           {-0.4f  , 0.5f },
           {-0.9f  ,-088f },
           { -0.84f,-1.17f}
        };

    public static float[,] characterPostion = new float[7, 2]
       {
           {8.16f,17.49f },
           {8.28f,17.49f },
           {8.22f,17.49f },
           {7.24f,17.49f },
           {7.73f,17.49f },
           {7.73f,17.49f },
           {7.76f,17.49f }
       };


}

