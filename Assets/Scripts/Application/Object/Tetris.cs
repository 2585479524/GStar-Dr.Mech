using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public GameObject[] tetrisGameObjects;

    public Transform downAndleft;

    public float RoTimer;

    public Animator land;

    private float Timer;
    bool crash = true;

    private BigBlock tetris;

    private Queue<BigBlock> tetrisQueue;

    private float x;
    private float y;

    private Vector3 MediumPos;

    public GameObject Failed;

    public GameObject Success;

    public GameObject Smokepa;

    public bool IsFailedUP = false;

    public PartCount PartCount;

    public Transform[] CreatPoint;

    public CanvasController canvasController;

    bool First = true;
    void Start()
    {
        RoTimer = ConstName.MoveDeltaTime;
        //计算生成位置
        x = downAndleft.position.x + Camera.main.orthographicSize - transform.localScale.x * 1.5f;
        y = downAndleft.position.y + (transform.localScale.y * 1.0f) / 2 + 2;
        MediumPos = new Vector3(x, y, 0);

        tetrisQueue = new Queue<BigBlock>();

        //tetrisQueue.Peek().BeBigger();
        //GetComponent<TurnTable>().CanCreat += FirstBiggerAndCreat;
    }


    public void SetStart()
    {
        for (int i = 0; i < 2; i++)
        {
            CreateTetris(CreatPoint[1 - i]);

        }
        tetrisQueue.Peek().transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Accelerate()
    {
        MapModel.IsAcce = true;
        RoTimer = ConstName.AcceDeltaTime;
        Timer = 0;
    }
    private void FixedUpdate()
    {
        //方块控制
        TetrisControl();
        if (tetris == null || crash == true)
        {
            return;
        }


        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            StartCoroutine(SendMes());
            Timer = RoTimer;
        }


        /*if (!IsFailedUP && tetris.transform.position.y > 25)
        {
            IsFailedUP = true;
            Failed.SetActive(true);
        }*/
    }
    private void CreateTetris(Transform follew)
    {
        GameObject te = Instantiate(tetrisGameObjects[randomNum()], follew.position, Quaternion.identity) as GameObject;
        //te.GetComponent<BigBlock>().Wait(follew);
        tetrisQueue.Enqueue(te.GetComponent<BigBlock>());
    }


    IEnumerator SendMes()
    {
        tetris.GetComponent<BigBlock>().MoveMes();
        yield return null;
    }
    /// <summary>
    /// 停下来接口
    /// </summary>
    public void Stop()
    {

        if (MapModel.IsPause)
        {
            return;
        }


        tetris.GetComponent<BigBlock>().SetLandBlocks();

        ///烟雾
        MapModel.CurrentShootBigBlock = null;

        tetris.GetComponent<BigBlock>().SetLandBlocks();


        tetris.GetComponent<BigBlock>().stop -= Stop;
        Debug.Log("停止控制");

        crash = true;


        if (MapModel.IsAcce)
        {
            land.SetTrigger("isShow");

            Game.Instance.Sound.PlayEffect("Acce_Sound", true);

            MapModel.IsAcce = false;

            this.SmokePlay();

        }
        else
        {

            Game.Instance.Sound.PlayEffect("Normal_Sound", true);

        }

        MapModel.deleteFullRows();
        MapModel.deleteFullColumn();

        if (MapModel.CurrentLevel == 1&&MapModel.FirstDelenum==3)
        {
            Success.SetActive(true);

            MapModel.IsPause = true;

            MapModel.CurrentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", MapModel.CurrentLevel);
        }

        //检测胜利
        bool Win = MapModel.GameWinOneCheck();
        if (Win)
        {
            Success.SetActive(true);

            MapModel.IsPause = true;
            //胜利添加数据
            MapModel.CurrentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", MapModel.CurrentLevel);

            Debug.Log("胜利");
        }

        RoTimer = ConstName.MoveDeltaTime;
        land.GetComponent<RotateCube>().stopRotate(crash);

        //删除零件

        //s Debug.Log("有零件消除");
        PartCount.ReText();

    }



    void SmokePlay()
    {
        foreach (var item in MapModel.LandBlocks)
        {
            GameObject te = Instantiate(Smokepa, new Vector3(item.GetIndex().x + 0.5f, item.GetIndex().y + 0.95f), Quaternion.identity) as GameObject;
            te.GetComponent<ParticleSystem>().playbackSpeed = 2f;
            te.GetComponent<ParticleSystem>().Play();
            te.transform.SetParent(item.transform);
            te.GetComponent<ParticleSystem>().transform.localScale = new Vector3(1f, 1f, 1f);
            te.GetComponent<ParticleSystem>().transform.Rotate(new Vector3(90f, 0f, -90f));
        }
    }




    public void StopAccelerate()
    {
        MapModel.IsAcce = false;
        RoTimer = ConstName.MoveDeltaTime;
        Timer = 0;
    }

    /// <summary>
    /// 发射出去
    /// </summary>
    public void Shoot()
    {
        if (!crash)
        {
            return;
        }
        tetris = tetrisQueue.Dequeue();

        tetris.transform.position = CreatPoint[1].position;

        GetComponent<TouchControl>().BigBlock = tetris;

        MapModel.CurrentShootBigBlock = tetris;

        tetris.GetComponent<BigBlock>().stop += Stop;
        tetris.GetComponent<BigBlock>().isUp += StopAccelerate;
        tetris.GetComponent<BigBlock>().Shoot();

        //GetComponent<TurnTable>().Rotate();
        crash = false;

        GameObject.Find("land").GetComponent<RotateCube>().stopRotate(crash);

        if (First)
        {
            First = false;

        }
        else
        {
            CreateTetris(CreatPoint[0]);
        }
    }




    private void TetrisControl()
    {

        if (MapModel.IsPause)
        {
            return;
        }
        // 生成 自动
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // 旋转 点击
        if (Input.GetKeyDown(KeyCode.S) && tetris)
        {
            tetris.GetComponent<BigBlock>().Ratate();
        }
        if (Input.GetKeyDown(KeyCode.W) && tetris)
        {
            Accelerate();
        }
        // 左右移动
        if (Input.GetKeyDown(KeyCode.A) && tetris)
        {
            tetris.GetComponent<BigBlock>().Left();
        }

        if (Input.GetKeyDown(KeyCode.D) && tetris)
        {
            tetris.GetComponent<BigBlock>().Right(); ;
        }

        /*if (Input.GetKeyDown(KeyCode.S) && tetris)
        {
            tetris.transform.position = new Vector3(tetris.transform.position.x, tetris.transform.position.y - 1, 0);
        }*/
    }
    private int randomNum()
    {
        return Random.Range(0, tetrisGameObjects.Length - 1);
    }
}
