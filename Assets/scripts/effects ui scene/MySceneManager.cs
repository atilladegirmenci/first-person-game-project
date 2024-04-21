using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
  
    static public MySceneManager instance;
    void Start()
    {
        instance = this;

        if(SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("DeathScene").buildIndex)
        {
            Cursor.visible = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("SampleScene").buildIndex)
        {
            Cursor.visible = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDeathScene()
    {
       
        SceneManager.LoadScene("DeathScene");
        
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");  
    }
    public void Quit()
    {
        Application.Quit();
    }
}
