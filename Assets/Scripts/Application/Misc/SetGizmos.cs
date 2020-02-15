using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于辅助画线
/// </summary>
public class SetGizmos : MonoBehaviour
{
    public Transform downAndleft;
    public Transform upAndright;
    public float interval = 0.5f;
    public bool IsDraw = false;
    public int width;
    public int hight;
    LineRenderer LineRenderer;
    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }
    void OnDrawGizmos()
    {
        if (!IsDraw || interval <= 0)
        {
            return;
        }



        Vector3 setNew = downAndleft.position;
        setNew.x = (int)setNew.x;
        setNew.y = (int)setNew.y;
        downAndleft.position = setNew;


        setNew = upAndright.position;
        setNew.x = (int)setNew.x;
        setNew.y = (int)setNew.y;
        upAndright.position = setNew;


        width = (int)(upAndright.position.x - downAndleft.position.x);
        hight = (int)(upAndright.position.y - downAndleft.position.y);
        Gizmos.color = Color.green;



        Vector3 down = downAndleft.position;
        Vector3 up = new Vector3(down.x, down.y + hight);
        while (up.x <= upAndright.position.x)
        {
            Gizmos.DrawLine(up, down);
            up.x += interval;
            down.x = up.x;
        }
        Vector3 left = downAndleft.position;
        Vector3 right = new Vector3(left.x + width, left.y);
        while (left.y <= upAndright.position.y)
        {
            Gizmos.DrawLine(left, right);
            left.y += interval;
            right.y = left.y;
        }

        if (MapModel.base_Grids != null)
        {
            Base_Grid[,] base_s = MapModel.base_Grids;
            for (int i = 0; i < ConstName.MapWidth; i++)
            {
                for (int j = 0; j < ConstName.MapHeight; j++)
                {
                    if (base_s[i, j].isHaveBlock)
                    {
                        Gizmos.color = Color.yellow;
                        Vector3 start = new Vector3(i, j, 0);
                        Vector3 end = new Vector3(i + 1, j + 1, 0);
                        Gizmos.DrawLine(start, end);
                    }
                }
            }
        }
    }
    public void ShowData()
    {
        /*Base_Grid[,] base_s = MapModel.base_Grids;
        for (int i = 0; i < ConstName.MapWidth; i++)
        {
            for (int j = 0; j < ConstName.MapHeight; j++)
            {
                if (base_s[i, j].isHaveBlock)
                {
                    LineRenderer.pos
                }
            }
        }*/
    }
}
