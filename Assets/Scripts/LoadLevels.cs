using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    public void LevelMenuStart()
    {
        SceneManager.LoadSceneAsync("LevelMenu");
        Time.timeScale = 1f;
    }
    public void StartLevelOne()
    {
        SceneManager.LoadSceneAsync("Level1");
        Time.timeScale = 1f;
    }
    public void StartLevelTwo()
    {
        SceneManager.LoadSceneAsync("Level2");
        Time.timeScale = 1f;
    }
    public void StartLevelThree()
    {
        SceneManager.LoadSceneAsync("Level3");
        Time.timeScale = 1f;
    }
    public void StartLevelFour()
    {
        SceneManager.LoadSceneAsync("Level4");
        Time.timeScale = 1f;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1f;
    }
    public void ControlsAndConditions()
    {
        SceneManager.LoadSceneAsync("Controls");
        Time.timeScale = 1f;
    }
    public void SettingsPage()
    {
        SceneManager.LoadSceneAsync("Settings");
        Time.timeScale = 1f;
    }
}