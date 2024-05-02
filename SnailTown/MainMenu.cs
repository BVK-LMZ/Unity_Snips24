using UnityEngine;
using UnityEngine.UI;

public class AudioAndButton : MonoBehaviour
{
    public AudioClip audioClip; // Reference to the audio clip you want to play
    public Button startButton; // Reference to the start button
    public Button endButton; // Reference to the end button

    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
             // Get or add the AudioSource component to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource and set it to loop
        audioSource.clip = audioClip;
        audioSource.loop = true;
       
        // Play the audio (assuming you want it to play as soon as the game starts)
        audioSource.Play();



        // Add listeners to the buttons
        startButton.onClick.AddListener(StartGame);
        endButton.onClick.AddListener(EndGame);
    }

    // Function to be called when the start button is clicked
    public void StartGame()
    {
        // Load the main game level here
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        Debug.Log("Main game level loaded!");
    }

    // Function to be called when the end button is clicked
    public void EndGame()
    {
        // Load the end game level here
        Debug.Log("End game level loaded!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Debug.Log("End game level loaded!");
    }
}
