using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    // Start is called before the first frame update


    float s = 0.5f;//0.397f;


    void Start()
    {
        //transform.position = new Vector3( MapModel.CurrentShootBigBlock.GetBeamPosition().postion_x -GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 +s,transform.position.y) ;
        //transform.localScale = new Vector3(MapModel.CurrentShootBigBlock.GetBeamPosition().length, transform.localScale.y);
       // GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
    }

    // Update is called once per frame
    void Update()
    {

        if (MapModel.CurrentShootBigBlock)
        {
            transform.position = new Vector3(MapModel.CurrentShootBigBlock.GetBeamPosition().postion_x + s * MapModel.CurrentShootBigBlock.GetBeamPosition().length, transform.position.y);
            // transform.position = new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + MapModel.CurrentShootBigBlock.GetBeamPosition().postion_x -s, transform.position.y);
            transform.localScale = new Vector3(MapModel.CurrentShootBigBlock.GetBeamPosition().length, transform.localScale.y);

        }
        else
        {
            transform.position = new Vector3(-10, transform.position.y);
            // transform.position = new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + MapModel.CurrentShootBigBlock.GetBeamPosition().postion_x -s, transform.position.y);
            transform.localScale = new Vector3(1, transform.localScale.y);

        }

        //transform.position = new Vector3(MapModel.CurrentShootBigBlock.GetBeamPosition().postion_x, transform.position.y);
        //transform.localScale = new Vector3(MapModel.CurrentShootBigBlock.GetBeamPosition().length, transform.localScale.y);
    }
}
