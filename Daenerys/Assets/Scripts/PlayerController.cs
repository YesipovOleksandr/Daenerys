using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Vector3 position;
    public float speed = 3.0F;
    public float jump = 5.0F;


    //проверка на земеле или нет 
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;
   

    void Start () {
       
	}
	

	void FixedUpdate() {
        CheckGround();
        if (Convert.ToBoolean(Input.GetAxis("Horizontal")))Run();
        if (isGrounded&&Input.GetButtonDown("Jump"))Jump();
      
    }

    private void Run()
    {
        position = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += position * speed * Time.deltaTime;
        if (position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    public void CheckGround()
    {
      //GameObject ground = GetComponentInChildren<GameObject>();
      isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,0.2F, whatIsGround);

    }
}
