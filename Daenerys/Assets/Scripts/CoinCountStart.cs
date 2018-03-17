using UnityEngine;
using UnityEngine.UI;

public class CoinCountStart : MonoBehaviour {

	// Use this for initialization
 private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
