using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Vector2 vel = new Vector2(-2f, 0);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnTriggerEnter2D(Collider2D other)
    {
        FireBallController fireBall = other.GetComponent<FireBallController>();
        if (fireBall)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
         
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        if (playerController)
        {
            gameObject.GetComponentInChildren<Rigidbody2D>().velocity = vel;
        }

    }

}
