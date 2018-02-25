using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpace : MonoBehaviour
{
    public GameObject RespawenPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            other.transform.position = RespawenPosition.transform.position;

        }
    }
}
