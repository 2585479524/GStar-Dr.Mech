using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Postion
{
    public int x;
    public int y;
}
[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
public class Game : ApplicationBase<Game>
{
    //全局访问功能
    [HideInInspector] public ObjectPool ObjectPool = null; //对象池
    [HideInInspector] public Sound Sound = null;           //声音控制
    [HideInInspector] public StaticData StaticData = null; //静态数据
    //public static SpriteRenderer[] characters = new SpriteRenderer[10];

    //全局方法
    public void LoadScene(int level)
    {
        //发布事件
        //SendEvent(ConstName.E_ExitScene, level);
        ExitScene(level);
        //---同步加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        SceneManager.sceneLoaded += LoadedEve;
    }
    void LoadedEve(Scene s, LoadSceneMode l)
    {
        if (l == LoadSceneMode.Single)
        {
            SceneManager.sceneLoaded -= LoadedEve;
            //事件参数
            int SceneIndex = s.buildIndex;

            //发布事件
            //SendEvent(ConstName.E_EnterScene, SceneIndex);
            EnterScene(SceneIndex);
        }
    }
    //游戏入口
    void Start()
    {
        //确保Game对象一直存在
        DontDestroyOnLoad(gameObject);

        //全局单例赋值
        ObjectPool = ObjectPool.Instance;
        Sound = Sound.Instance;
        StaticData = StaticData.Instance;

        //注册启动命令
        RegisterController(ConstName.E_StartUp, typeof(StartUpCommand));

        //启动游戏

        //PlayerPrefs.SetInt("CurrentLevel", 1);
        if (PlayerPrefs.GetInt("CurrentLevel", -1)==-1)
        {
            Debug.Log("第一次游戏");
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        //PlayerPrefs.SetInt("CurrentLevel", 3);
        /*characters = Resources.LoadAll<SpriteRenderer>("Character/") as SpriteRenderer[];
        Debug.Log(characters.Length);*/
        LoadScene(1);

    }
    private  void  ExitScene(int data)
    {
        int index = (int)data;
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    private void EnterScene(int data)
    {
        int index = (int)data;
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:

                if (MapModel.CurrentLevel==6)
                {
                    LoadScene(4);
                    Instance.Sound.PlayBg("End");
                }
                MapModel.InitMapModel();


                if (MapModel.CurrentLevel>1)
                {
                    Transform chara = GameObject.Find("Circle").transform.GetChild(1);
                    Debug.Log(1);
                    chara.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Character/c"+ MapModel.CurrentLevel);
                }
                
               /* float x = ConstName.characterScale[MapModel.CurrentLevel,0];
                float y = ConstName.characterScale[MapModel.CurrentLevel,1];
                Vector3 vector3 = new Vector3(x, y, 0);
                chara.localScale = vector3;*/
                Instance.Sound.PlayBg("BGM");

                MapModel.IsPause = true;

                break;
            case 3:
                MapModel.InitMapModel();
                break;
        }
    }
    public Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>(name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

