using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    private float recordVolume = 0f;
    private float recordSoundEffect = 0f;

    // Start is called before the first frame update
    void Start()
    {
        recordVolume = Game.Instance.Sound.BgVolume;
        recordSoundEffect = Game.Instance.Sound.EffectVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 暂停与继续
    public void PauseOrContinue(bool isPause)
    {
        if (isPause)
        {
            // 暂停
            Time.timeScale = 0;
            MapModel.IsPause = true;
        }
        else
        {
            // 继续
            Time.timeScale = 1;
            MapModel.IsPause = false;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ReNew()
    {
        Game.Instance.LoadScene(2);
    }
    public void ShowSettings(bool isShow)
    {
        gameObject.SetActive(isShow);

    }
    public void changeBGM(bool isOpen)
    {
        if (isOpen)
        {
            Game.Instance.Sound.BgVolume = 0;
        }
        else
        {
            Game.Instance.Sound.BgVolume = recordVolume;
        }
    }
    public void ChangeSoundEffect(bool isOpen)
    {
        if (isOpen)
        {
            Game.Instance.Sound.EffectVolume = 0;
        }
        else
        {
            Game.Instance.Sound.EffectVolume = recordVolume;
        }
    }

}