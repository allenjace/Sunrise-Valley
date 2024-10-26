using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    public void LevelMenuStart()
    {
        SceneManager.LoadSceneAsync("LevelMenu");
        Time.timeScale = 1;
    }
    public void StartLevelOne()
    {
        SceneManager.LoadSceneAsync("LevelOne");
        Time.timeScale = 1;
    }
    public void StartLevelTwo()
    {
        SceneManager.LoadSceneAsync("LevelTwo");
        Time.timeScale = 1;
    }
    public void StartLevelThree()
    {
        SceneManager.LoadSceneAsync("LevelThree");
        Time.timeScale = 1;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void SettingsPage()
    {
        SceneManager.LoadSceneAsync("Settings");
    }
}