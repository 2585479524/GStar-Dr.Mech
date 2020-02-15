using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ax : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<SpriteRenderer>().bounds.extents.x);   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
