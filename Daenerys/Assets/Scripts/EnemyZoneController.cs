using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneController : MonoBehaviour {
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        transform.position = GetComponentInChildren<Rigidbody2D>().transform.position;
	}
  
    
}
