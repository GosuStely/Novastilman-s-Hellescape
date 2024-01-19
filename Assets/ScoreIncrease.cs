using UnityEngine;
using TMPro;

public class ScoreIncrease : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    void Update()
    {
        if (!PauseMenu.IsItPaused())
        {
            score += (int)Time.time;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
