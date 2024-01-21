using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void GoToSettingMenu()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
