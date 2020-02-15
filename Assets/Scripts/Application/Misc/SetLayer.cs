using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    public SpriteRenderer land;
    public delegate void End();
    public End PlayEnd;
    public void IsEnd()
    {
        PlayEnd();
    }

    public void ChangeLand2Layer()
    {
        Debug.Log("land2");
        land.sortingLayerName = "land2";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
