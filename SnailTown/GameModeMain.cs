using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameModeMain : MonoBehaviour
{
    public static GameModeMain Instance { get; private set; }

    // Add a public variable to store the TMP text component
    public TextMeshProUGUI scoreText;

    // Add a public variable to store the score
    public int score = 0;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If not, set this instance as the singleton
            Instance = this;
            // Ensure that this GameObject persists between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }

    // Add a function to handle when a snail dies
    public void OnSnailDeath()
    {
        // Increment the score
        score++;
        // Update the TMP text component
        scoreText.text = "Score: " + score;

        // Check if the score is greater than 4
        if (score <= 2)
        {
            // If so, load the next level
            LoadNextLevel();
        }
    }

    // Add a function to load the next level
    void LoadNextLevel()
    {
       
         SceneManager.LoadScene("EndGame");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
