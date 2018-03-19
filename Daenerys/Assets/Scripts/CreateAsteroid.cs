using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAsteroid : MonoBehaviour
{
    //Player
    [SerializeField]
    private Transform target;
    GameObject asteroid;
 


    void Start()
    {
        asteroid = Resources.Load<GameObject>("Asteroid");
        InvokeRepeating("createAsteroid", 5F, 4F);
    }


    
    void createAsteroid()
    {    
       Instantiate(asteroid, new Vector3(Random.Range(target.position.x , target.position.x + 20), 9F, 0F), Quaternion.identity);
    }
  

}