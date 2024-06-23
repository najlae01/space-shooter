using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void OnEnable()
    {
        int score = PlayerPrefs.GetInt("Score" + SceneManager.GetActiveScene().name, 0);
        scoreText.text = "SCORE: " + score.ToString();

        int highscore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }
}
