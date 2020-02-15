using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum Direction { on, down, left, right };
    #region 字段
    private Direction m_currenDirection;
    private List<Block> m_hasLandBlocks = new List<Block>();
    public int StartIndex_x = 5;
    public int StartIndex_y = 6;
    public int Lenth = 9;

    public SetGizmos SetGizmos;
    public Transform point1;
    public Transform point2;

    #endregion
    /*private void OnDrawGizmos()
    {
        StartIndex_x = SetGizmos.width / 2 - Lenth / 2 - 1;
        StartIndex_y = SetGizmos.hight / 2 - Lenth / 2 + 4;
    }*/

    #region 属性

    public Direction CurrenDirection
    {
        set
        {
            m_currenDirection = value;
        }
        get
        {
            return m_currenDirection;
        }
    }


    public List<Block> HasLandBlocks
    {
        set
        {
            m_hasLandBlocks = value;
        }
        get
        {
            return m_hasLandBlocks;
        }
    }


    #endregion




    #region 方法

    #endregion
}
