using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    private Text Money;
    // Use this for initialization
    void Start () {
        Money= GameObject.Find("Count").GetComponent<Text>();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            Money.text = PlayerPrefs.GetInt("Coin").ToString();
        }
    }
}
