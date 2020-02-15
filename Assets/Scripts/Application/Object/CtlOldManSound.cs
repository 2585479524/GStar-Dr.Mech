using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtlOldManSound : MonoBehaviour
{

    public  void playHammer()
    {
        Game.Instance.Sound.PlayEffect("Hammer", false);
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
