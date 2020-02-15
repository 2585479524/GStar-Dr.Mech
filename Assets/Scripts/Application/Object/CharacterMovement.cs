using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveDistance = 2;
    
    public int moveDirection = -1;


    public float jumpHeight = 0.2f;
    public float ySpeed = 0.02f;
    public float xSpeed = 1.0f;
    private int dir;
    private float x, y;
    private float distance;
    private float y0;
    void Start()
    {
        dir = 1;
        if (moveDirection == 1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        distance = 0;
        y0 = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (distance > moveDistance)
            return;
        x = transform.position.x + moveDirection *xSpeed * Time.deltaTime;
        distance += Mathf.Abs(xSpeed * Time.deltaTime);
        y = transform.position.y +  dir * ySpeed;
        if (y >= y0 + jumpHeight || y <= y0)
        {
            dir = -dir;
        }
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void GoBack() {
        distance = 0;
        moveDirection = -moveDirection;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
