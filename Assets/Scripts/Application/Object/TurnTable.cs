using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable : MonoBehaviour
{
    public Transform[] Points;

    private float angle = 0;
    public float speed = 0.1f;

    public  bool CanRote = false;

    private int index = 3;

    public int Index
    {
        set
        {
            index = value % Points.Length;
        }
        get
        {
            return index;
        }
    }
    public delegate void IsStop(Transform tr);
    public IsStop CanCreat;
    // Start is called before the first frame update
    void Awake()
    {
        //Cos30 = Mathf.Cos(Mathf.PI / 6);
    }
    public void Rotate()
    {
        Invoke("StartRote", 1f);
    }
    public void StartRote()
    {
        
        CanRote = true;
        angle += 90;      
        Invoke("StopRota", 1.2f);
    }
    // Update is called once per frame
    public void StopRota()
    {   
        
        CanRote = false;
        
        CanCreat(GetCreatPoint());

    }
    public Transform GetCreatPoint()
    {
        return Points[Index++];
    }
    void Update()
    {
        if (CanRote)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), speed);
        }
    }
}
