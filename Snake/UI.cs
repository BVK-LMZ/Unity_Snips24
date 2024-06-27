using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI : MonoBehaviour
{
    // Serialized field to display the score
    [SerializeField] private TextMeshProUGUI scoreText;


    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // This is where you could handle any real-time UI updates if necessary
    }

    // Method to add to the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Method to reset the score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Private method to update the score text UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
