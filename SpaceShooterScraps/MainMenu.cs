using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // --- UI Elements ---
    [Header("UI Elements")]
    [SerializeField] private Button _playButton;              // Button to start Level 1
    [SerializeField] private Button _levelPanelButton;        // Button to open/close Level Panel
    [SerializeField] private Button _loadLevelOneButton;      // Button to load Level 1
    [SerializeField] private Button _loadLevelTwoButton;      // Button to load Level 2
    [SerializeField] private Button _quitButton;              // Button to quit the game
    [SerializeField] private GameObject _levelPanel;          // Level selection panel

    // --- Initialization ---
    private void Start()
    {
        // Adding listeners to buttons
        _playButton.onClick.AddListener(LoadLevelOne);
        _levelPanelButton.onClick.AddListener(ToggleLevelPanel);
        _loadLevelOneButton.onClick.AddListener(LoadLevelOne);
        _loadLevelTwoButton.onClick.AddListener(LoadLevelTwo);
        _quitButton.onClick.AddListener(QuitGame);

        // Ensure the Level Panel is hidden at start
        HideLevelPanel();

        Debug.Log("MainMenu initialized. Level Panel is hidden at start.");
    }

    // --- Function to Load Level 1 ---
    private void LoadLevelOne()
    {
        Debug.Log("Loading Level 1...");
        SceneManager.LoadScene("Level1");
    }

    // --- Function to Load Level 2 ---
    private void LoadLevelTwo()
    {
        Debug.Log("Loading Level 2...");
        SceneManager.LoadScene("Level2");
    }

    // --- Function to Quit the Game ---
    private void QuitGame()
    {
        Debug.Log("Quitting the game...");
        // For testing purposes in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // --- Function to Show the Level Panel ---
    private void ShowLevelPanel()
    {
        _levelPanel.SetActive(true);
        Debug.Log("Level Panel shown.");
    }

    // --- Function to Hide the Level Panel ---
    private void HideLevelPanel()
    {
        _levelPanel.SetActive(false);
        Debug.Log("Level Panel hidden.");
    }

    // --- Function to Toggle the Level Panel ---
    private void ToggleLevelPanel()
    {
        _levelPanel.SetActive(!_levelPanel.activeSelf);
        Debug.Log("Level Panel toggled. Now active: " + _levelPanel.activeSelf);
    }
}
