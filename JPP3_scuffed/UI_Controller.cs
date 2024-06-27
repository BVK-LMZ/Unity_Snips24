using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_Controller : MonoBehaviour
{
    public TextMeshProUGUI resetText;
    public TMP_Text coinsText;

    public Animator animator;

    private int coins = 0;
    private bool isPaused = false;

    void Start()
    {



        resetText.text = "";


        coinsText.text = "Coins: 0";
        ResetText();
        InitCoins();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
          
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void ResetText()
    {
        resetText.fontSize = 50;
        coinsText.text = "Coins: " + coins.ToString();
    }



    void InitCoins()
    {
        coins = 0;
        coinsText.text = "Coins: " + coins.ToString();
    }

    void ResetLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        if(isPaused)
        {
            TogglePause();
        }
    }

    public void player_dead()
    {
        resetText.text = "PRESS R TO RESET";

        //anim set dead
        animator.SetBool("bIsDead", true);




    }

    public void UpdateCoins()
    {
        coins++;
        coinsText.text = "Coins: " + coins.ToString();
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
          


        }
        else
        {
            Time.timeScale = 1f; // Resume normal time scale
            Debug.Log("Game Resumed");
            // Optionally: Hide pause menu or overlay UI
        }
    }

  
}
