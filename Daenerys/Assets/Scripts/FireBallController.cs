using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set{ direction = value; } }

	void Start () {
        Destroy(gameObject,2F);
	}

	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        } 
    }
}
