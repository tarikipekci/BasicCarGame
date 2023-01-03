using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    [Header("Components")] public Text timeCounter;

    [Header("UI")] public GameObject CrashScreen;

    [Header("Variables")] public static float bestTime = 0;
    public static float time;
    
    private void Update()
    {
        if (bestTime < time)
        {
            bestTime = time;
        }
        
        if (CarController.instance.crashCounter != 1)
        {
            timeCounter.text = "Time = " + Mathf.Round( Time.timeSinceLevelLoad) + "\nBest Time: " + Mathf.Round(bestTime);
        }

        if (CarController.instance.crashCounter == 1)
        {
            timeCounter.gameObject.SetActive(false);
            time = Time.timeSinceLevelLoad;
            Time.timeScale = 0f;
            CrashScreen.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                timeCounter.gameObject.SetActive(true);
                Time.timeScale = 1f;
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}