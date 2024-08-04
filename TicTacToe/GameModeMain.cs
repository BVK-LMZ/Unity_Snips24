using UnityEngine; //Oh Yeah, UNITY
using TMPro; // Import the TextMeshPro namespace to use TMP_Text
using UnityEngine.UI; // Import the UnityEngine.UI namespace to use Button
using UnityEngine.SceneManagement;  // Import this to use SceneManager

//Create a grid of 8 elements for this to work!
public class GameModeMain : MonoBehaviour
{
    public Button[] buttons; // Public array to assign TextMeshPro buttons in the Unity Inspector

    
    private string currentPlayer = "X"; // Variable to keep track of the current player, either "X" or "O", starting with X

    //Called in the start func, Initialize the grid by setting up each button
    // Set up button grid by going through all buttons
    // For all buttons a a listner to call Onbutton click func
    // And Clear the Text for all buttons
    void InitializeGrid()
    {
        // Loop through each button in the buttons array
        foreach (Button button in buttons)
        {
            // Add an event listener to each button that calls OnButtonClick when clicked
            button.onClick.AddListener(() => OnButtonClick(button));

            // Get the TextMeshPro text component from the button
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            // Clear any text from the button to ensure it's empty at the start
            buttonText.text = "";
        }
    }

    //This function runs when a button is clicked.
    // It retrieves the text component from the clicked button.
    // If the text is empty(i.e., no symbol is set), it assigns the current player's symbol ("X" or "O") to the button.
    // If the button already has a symbol(i.e., it's not empty), the function does nothing, leaving the symbol unchanged, and waits for an empty button to be clicked before making a move and switching players.
    // It then checks if this move results in a win or a draw using the CheckForWin function.
    // If no win is detected, it switches to the other player's symbol.
    // The game continues with the updated player symbol.
    void OnButtonClick(Button button)
    {

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();// Get the TextMeshPro text component from the clicked button

        if (buttonText.text == "")  // Check if the button's text is empty (i.e., it hasn't been clicked yet)
        {
            buttonText.text = currentPlayer;  // Set the button's text to the current player's symbol ("X" or "O")
            CheckForWin();// Check if this move results in a win or a draw
            SwitchPlayer();// Switch the turn to the other player
        }
    }

    //This function Switch the current player between "X" and "O"
    //It is called  at the end of OnButtonClick 
    void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == "X") ? "O" : "X"; // Change the current player to the other symbol
    }

    // Check winning for a winning combination, from the grid {0-8}
    // Using winning combinations
    void CheckForWin()
    {
        // Array of winning patterns, each pattern is a string of button indices
        string[] winPatterns =
        {
            "012", // Winning row: buttons 0, 1, 2
            "345", // Winning row: buttons 3, 4, 5
            "678", // Winning row: buttons 6, 7, 8
            "036", // Winning column: buttons 0, 3, 6
            "147", // Winning column: buttons 1, 4, 7
            "258", // Winning column: buttons 2, 5, 8
            "048", // Winning diagonal: buttons 0, 4, 8
            "246"  // Winning diagonal: buttons 2, 4, 6
        };

        // Loop through each winning pattern to check if it is satisfied
        // REMEMBER that is a FOR EACH, so there are various iterations!
        foreach (string pattern in winPatterns)
        {
            // From the STRING of WINS, extract winning combo and store in number
            // From the STRING of WINS, extract winning combo and store in numbered, function local indexes of 3.
            // While it is 1-3, it represents the various patterns defined above !
            int index1 = int.Parse(pattern[0].ToString());
            int index2 = int.Parse(pattern[1].ToString());
            int index3 = int.Parse(pattern[2].ToString());

            // Get the TextMeshPro text component from each button in the pattern
            // Take the index provided above and set the texts equal to the current value of the board
            // in relation to the index provided above
            TMP_Text text1 = buttons[index1].GetComponentInChildren<TMP_Text>();
            TMP_Text text2 = buttons[index2].GetComponentInChildren<TMP_Text>();
            TMP_Text text3 = buttons[index3].GetComponentInChildren<TMP_Text>();

            // Check if all three buttons in the pattern have the same text
            // text1.text != "", IS Important to ensure there is no blank combination that wins!!
            if (text1.text == text2.text && text2.text == text3.text && text1.text != "")
            {
                // If the texts match, declare the winner based on the symbol
                Debug.Log($"{text1.text} wins!"); // Print the winner to the console


                return; // Exit the method once a win is detected
            }
        }

        //AFTER WIN COMBO CHECK RUN THIS NEXT PART


        //FUNC specific variable, that is not saved in the class and reset on EACH ITERATION
        bool isDraw = true;  // Initialize a boolean to track if all buttons are filled

        foreach (Button button in buttons) // Loop through each button to see if there are any empty spots left
        {
            
            if (button.GetComponentInChildren<TMP_Text>().text == "")// If any button is still empty, it's not a draw
            {
                isDraw = false;
                break; // Exit the loop if a blank button is found
            }
        }
        // If all buttons are filled and no winner, declare a draw
        if (isDraw)
        {
            Debug.Log("It's a draw!"); // Print a draw message to the console
        }
    }


    ///
    //MB
    ///

    // INNIT BOARD ON SCENE START
    void Start()
    {
       
        InitializeGrid();// Initialize the grid of buttons when the game starts
    }

    // Check for level reset
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Check if the "R" key is pressed
        {
            // Reload the current scene
            Scene currentScene = SceneManager.GetActiveScene(); // Get Current Scene
            SceneManager.LoadScene(currentScene.name);  // Load Current Scene
        }
    }
}
