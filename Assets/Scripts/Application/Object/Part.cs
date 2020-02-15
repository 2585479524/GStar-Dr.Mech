using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    private Vector3 startPos;

    private Vector3 endPos;

    private Vector3 startScale;

    private Vector3 endScale;

    private bool tag1 = false;


    public bool trigger;
    // private bool IsArr2=false;

    private
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       /* if (trigger)
        {
            Debug.Log("触发动画");
            getPartAnim();
        }*/
        if (trigger)
        {

            if (tag1)
            {
                transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 5f);

                if (Mathf.Abs((endPos - transform.position).sqrMagnitude) < 0.05f)
                {
                    transform.position = endPos;
                    trigger = false;

                    Game.Instance.Sound.PlayEffect("part", false);

                    GameObject.Find("Circle").transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hammer1");

                    Destroy(gameObject);
                }
            }
           

            transform.localScale = Vector3.Lerp(transform.localScale, endScale, Time.deltaTime * 2f);

            if (Mathf.Abs((endScale - transform.localScale).sqrMagnitude) < 0.1f)
            {
                endScale = startScale;
                tag1 = true;
            }

        }


    }

    public void Trigger()
    {
     
        transform.SetParent(null);

        //startPos = transform.position;
        endPos = GameObject.Find("land").transform.position;

        startScale = transform.localScale;
        endScale = transform.localScale + new Vector3(0.5f, 0.5f, 0);

      
        trigger = true;
    }


   /* public void getPartAnim()
    {

        Zoom(endScale);

        if (tag2)
        {
            tag2 = true;
            MoveToCenter(endPos);
            if (tag1)
            {
                tag1 = false;
                Debug.Log("缩小始末位置" + transform.localScale + " " + startScale);
                Zoom(startScale);
                if (tag2)
                {
                    tag2 = false;
                    
                    Destroy(gameObject);
                }
            }
        }
    }*/

    // 移动到中心
    /*void MoveToCenter(Vector3 targetPos)
    {
        if (Mathf.Abs((targetPos - transform.position).sqrMagnitude) < 0.05f)
        {
            transform.position = targetPos;

            tag1 = true;
            return;

        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
    }

    // 缩放
    void Zoom(Vector3 targetScale)
    {
        if (Mathf.Abs((targetScale - transform.localScale).sqrMagnitude) < 0.1f)
        {
            transform.localScale = targetScale;

            tag2 = true;
            return;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 3f);
        Debug.Log("zoom" + transform.localScale);
    }*/
}
