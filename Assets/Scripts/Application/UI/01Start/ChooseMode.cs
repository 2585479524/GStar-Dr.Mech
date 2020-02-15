using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMode : View
{
    public override string Name
    {
        get
        {
            return ConstName.V_Start;
        }
    }
    private void Start()
    {

    }
    public void StoryMode()
    {
        Game.Instance.LoadScene(2);
    }
    public void EndlessMode()
    {
        Game.Instance.LoadScene(3);
    }
    public override void HandleEvent(string eventName, object data)
    {
        
    }
}
