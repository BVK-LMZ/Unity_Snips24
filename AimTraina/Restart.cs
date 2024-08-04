using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Call the RestartCurrentLevel method
            RestartCurrentLevel();
        }
    }

    // This method restarts the current level
    void RestartCurrentLevel()
    {
        // Get the currently loaded scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
