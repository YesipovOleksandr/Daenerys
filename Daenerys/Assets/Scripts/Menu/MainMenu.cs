using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource DarkSideAudio;
    public AudioSource BrightAudio;


  

    public void DarkSide()
    {
        DarkSideAudio.Play();
        Invoke("SceneManagerDarkSide", 3);
      
    }
    private void SceneManagerDarkSide()
    {
        SceneManager.LoadScene(2);
    }
    public void BrightSide()
    {
        BrightAudio.Play();  
        Invoke("SceneManagerBrightSide",4);
    }

    private void SceneManagerBrightSide()
    {
        SceneManager.LoadScene(1);
    }


}
