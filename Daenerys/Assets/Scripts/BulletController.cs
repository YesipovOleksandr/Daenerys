using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private float speed = 15.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    void Start()
    {
        Destroy(gameObject, 1F);
    }
    void Update()
    {
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
