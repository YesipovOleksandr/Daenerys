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

        Layer1.mainTextureOffset = new Vector2(Time.time * speed / 2, 0F);
        Layer2.mainTextureOffset = new Vector2(target.position.x*speed*2*Time.deltaTime, 0F);
        Layer3.mainTextureOffset = new Vector2(target.position.x * Time.deltaTime * speed, 0F);
    }
}
