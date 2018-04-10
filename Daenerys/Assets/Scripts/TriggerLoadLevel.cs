using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoadLevel : MonoBehaviour {

    public int Level;

	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Application.LoadLevel(Level);
        }
    }
}
