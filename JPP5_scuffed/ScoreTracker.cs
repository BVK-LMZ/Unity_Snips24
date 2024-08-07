using UnityEngine;
using TMPro; // Import TextMeshPro namespace for UI text components
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement; // Include this namespace to use SceneManager

public class ScoreTracker : MonoBehaviour
{

    // Public variable to assign TextMeshProUGUI component from the inspector
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextFinal;
    public GameModeMain _gmm;



    //buttons
    public Button _end;
    public GameObject _endCanvas;


    public int _targetsMissed;

    // Private variable to store the player's score
    private int _score;

    // Called when the script instance is being loaded
    void Start()
    {
        // Initialize the score to 0
        _score = 0;

        // Update the scoreText to display the initial score
        UpdateScore(0);

        _end.onClick.AddListener(OnEndClick);
    }

    private void OnEndClick()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);

    }


  
       
      



        // Method to update the score and scoreText
        public void UpdateScore(int scoreToAdd)
    {
        // Increment the score by the given amount
        _score += scoreToAdd;

        // Update the scoreText to display the current score
        scoreText.text = "Score: " + _score;
    }

    // This method simulates spawning a target and calling UpdateScore
    public void SpawnTarget()
    {
        // Call UpdateScore with 5 to add 5 points to the score
        UpdateScore(5);
    }

    public void  MissedTarget()
    {
        if (_targetsMissed >= 5)
        {
            print("missed a bunch of targets");
            _gmm._bSpawnObj = false;
            _endCanvas.SetActive(true);
            scoreTextFinal.text = "Final Score: " + _score;
        }
        _targetsMissed++;
    
    }




}
