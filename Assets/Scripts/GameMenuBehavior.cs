using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuBehavior : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        Score.totalScore = 0;
        GameManager.points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void RestartGame(){
        Score.totalScore = 0;
        GameManager.points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

    public void ExitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
