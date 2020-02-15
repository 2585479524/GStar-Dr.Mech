using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public GameObject Failed;


    private float angle = 0;

    public float speed = 0.1f;

    private float Timer ;

    private bool isTime;

    private bool isShoot = false;

    private float ReDaTime;
    //private bool isRotate = true;
    // Start is called before the first frame update
    void Start()
    {
        Timer = ConstName.RotateDeltaTime;

        ReDaTime = ConstName.ReDataDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Interval();
        ReDaTime -= Time.deltaTime;
        if (ReDaTime<=0)
        {
            ReDaTime = ConstName.ReDataDeltaTime;
            ReData();
        }
 
    }

    public void stopRotate(bool carsh)
    {
        isShoot = !carsh;
        //isRotate = true;
        if (isShoot)
        {
            Invoke("ReData", 0.4f);
        }
    }
    public void ReData()
    {
        MapModel.Redata();
        bool Loser = MapModel.GameOverCheck();
        if (Loser)
        {
            Failed.SetActive(true);
            MapModel.IsPause = true;
            Debug.Log("输了");
        }
    }
    private void Interval()
    {
        if (isShoot)
        {
            Timer = 0.7f;
        }
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = ConstName.RotateDeltaTime;
            isTime = true;
            angle += 90;
            //Invoke("ReData", 0.5f);
        }
        if (isTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), speed);
        }

        //isRotate = false;
    }

}