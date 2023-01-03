using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
