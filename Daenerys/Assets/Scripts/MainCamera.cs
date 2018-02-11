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
        position.z = -20.0F;
        position.y =+1.2F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
   
    }
}
