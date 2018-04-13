using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

  

    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;


    private void FixedUpdate()
    {
        Vector3 position = target.position;
        position.z = -10.0F;
        position.y =+2F;
        //граница слева
        if (position.x <=0F)
        {
            position.x =0;
        }
        //граница справа
        if (position.x >= 145)
        {
            position.x = 145;
        }
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
   
    }

    private void Start()
    {
        //ландшафтная ориентация 
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
