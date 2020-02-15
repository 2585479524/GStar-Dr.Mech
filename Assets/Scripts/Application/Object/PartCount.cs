using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartCount : MonoBehaviour
{
    public Text[] Count;
    // Start is called before the first frame update
    void Start()
    {
        ReText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReText()
    {
        for (int i = 0; i < Count.Length; i++)
        {
            Count[i].text = MapModel.PartsCount[i].ToString();
        }
    }
}
