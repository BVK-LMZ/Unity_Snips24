using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    void Update()
    {
        // Check for input to reset the level (e.g., pressing the "R" key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene(); //get scene
            SceneManager.LoadScene(currentScene.name); //load scene
        }
    }


}
