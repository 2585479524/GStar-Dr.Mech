using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIScale : MonoBehaviour
{
    private void Awake()
    {
        if (Screen.width > 1200)
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
    }
}
