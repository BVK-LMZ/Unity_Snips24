using UnityEngine;
using UnityEngine.SceneManagement; // Add this to use SceneManager

public class RtoReset : MonoBehaviour
{
    void Update()
    {
        // Check if the "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentLevel();
        }
    }

    // Method to reset the current level
    private void ResetCurrentLevel()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
