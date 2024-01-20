using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Map");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
