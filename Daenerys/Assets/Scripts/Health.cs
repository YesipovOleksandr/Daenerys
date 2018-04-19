using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float curHealth = 200;
    public GameObject healtObj;
    public Text heathText;

   

    void Update () {
        healtObj.transform.localScale = new Vector3(curHealth / 200, 1, 1);
        heathText.text = "+" + curHealth.ToString("0");

        if (curHealth >= 200)
        {
            curHealth = 200;
        }

        if (curHealth < 0)
        {        
            curHealth = 0;
            Death();

        }
	}

    public void AddHealth(float health)
    {
        float allHealth = curHealth + health;


        if (allHealth >= 200)
        {
            curHealth = 200;
           
        }
        else
        {
            curHealth += health;
        }
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }

    void Death()
    {   
        string sceneName = SceneManager.GetActiveScene().name;
        // Загружаем её саму родимую
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //монетки обнуляем
      
        PlayerPrefs.DeleteKey("Coin");
    }
}
