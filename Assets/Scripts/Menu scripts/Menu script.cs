using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("OpeningScene");
    }
    public void GoToSettingMenu()
    {
        SceneManager.LoadSceneAsync(2);
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
