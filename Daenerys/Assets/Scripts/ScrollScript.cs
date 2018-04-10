using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    public Material [] Layers ;

    public Transform target;


    public float speed = 0;
    void Start()
    {

    }

    void FixedUpdate()
    {

        // Layer1.mainTextureOffset = new Vector2(Time.time * speed / 2, 0F);
        //Vector3  position = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f);
        //  if (position.x> 0)
        //  {
        //      //  Layer1.mainTextureOffset = new Vector2(position.x*Time.deltaTime* speed*2, 0F);

        //  }
        //  else
        //  {
        //      Layer1.mainTextureOffset = new Vector2(Time.time * speed / 3, 0F);
        //  }
        // print(target.position.x);
        Layers[0].mainTextureOffset = new Vector2(Time.time * speed / 2, 0F);
        Layers[1].mainTextureOffset = new Vector2(target.position.x * speed * 2 * Time.deltaTime, 0F);
        Layers[2].mainTextureOffset = new Vector2(target.position.x * Time.deltaTime * speed, 0F);
        Layers[3].mainTextureOffset = new Vector2(target.position.x * Time.deltaTime * speed, 0F);
        Layers[4].mainTextureOffset = new Vector2(target.position.x * Time.deltaTime * speed / 2, 0F);

    }
}