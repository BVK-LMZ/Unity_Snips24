//https://www.youtube.com/watch?v=K362yUU9vJk&list=PLZ1b66Z1KFKjbczUeqC4KYpn6fzYjLKoV
//set up buttons to the script
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolNotePad : MonoBehaviour
{
    [Header("Buttons")]
    public Button closeButton;
    public Button clearButton;
    public Button saveButton;

    [Header("Input Field")]
    public TMP_InputField textInputField;



    void Start()
    {
        SetWindowSize(800, 600);  // Example initial window size
        LoadSavedText();

        // Attach button click handlers
        closeButton.onClick.AddListener(HandleCloseButton);
        clearButton.onClick.AddListener(HandleClearButton);
        saveButton.onClick.AddListener(HandleSaveButton);

    }

    void HandleCloseButton()
    {
        Debug.Log("Close button clicked. Quitting application...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void HandleClearButton()
    {
        Debug.Log("Clear button clicked. Clearing input field...");
        textInputField.text = "";
    }

    public void HandleSaveButton()
    {
        string textToSave = textInputField.text;
        PlayerPrefs.SetString("SavedText", textToSave);
        PlayerPrefs.Save();

        Debug.Log("Saved text: " + textToSave);
    }

    void LoadSavedText()
    {
        string savedText = PlayerPrefs.GetString("SavedText", "");
        textInputField.text = savedText; // Load saved text into input field

        Debug.Log("Loaded text: " + savedText);
    }
    private void SetWindowSize(int width, int height)
    {
        Screen.SetResolution(width, height, false);  // Set the resolution and windowed mode
    }
}

