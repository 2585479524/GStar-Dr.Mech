using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MapModel.CurrentLevel!=1)
        {
            Destroy(gameObject);
        }
    }
}
