using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

   

	void FixedUpdate() {
        //под углом
        transform.position += -transform.right * Time.deltaTime * 5F;


    }

    void OnTriggerEnter2D(Collider2D other)
    {
      

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
            SpecialEffectsHelper.Instance.Explosion(transform.position);
        }
    }
}
