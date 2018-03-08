using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoEnemy : MonoBehaviour {
    private EnemyController enemy;
    // Use this for initialization
    void Start () {
        enemy = Resources.Load<EnemyController>("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(CreateEnemy());
                print("r");
            }

        }
    }

    IEnumerator CreateEnemy()
    {
       
            Instantiate(enemy, new Vector3(17.45F, -2.04F), Quaternion.identity);
            yield return new WaitForSeconds(5F);
        


    }
}
