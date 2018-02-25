using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollSctipt : MonoBehaviour {

   public Material Layer1;

    public Material Layer2;

    public Material Layer3;

    public Transform target;


    public float speed = 0;
    void Start () {

    }

	void FixedUpdate() {

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
        Layer1.mainTextureOffset = new Vector2(Time.time * speed / 2, 0F);
        Layer2.mainTextureOffset = new Vector2(target.position.x*speed*2*Time.deltaTime, 0F);
        Layer3.mainTextureOffset = new Vector2(target.position.x * Time.deltaTime * speed, 0F);
     
    }
}
