using UnityEngine;

public class GameModeMain : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab; // Use prefab to instantiate new objects
    [SerializeField] private Food _foodPrefab; // Use prefab to instantiate new objects
    [SerializeField] private UI _ui;

    public bool _bGameIsPlaying = false;

    private Player _playerInstance; // Keep reference to the instantiated player
    private Food _foodInstance; // Keep reference to the instantiated food

    public int score; // Current score of the player
    private int highscore; // Highscore stored in PlayerPrefs

    void ClearAllContent()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject food in foods)
        {
            Destroy(food);
        }
    }

    void SpawnAllContent()
    {
        // Define spawn positions LOCALLY
        Vector3 playerSpawnPosition = new Vector3(0, 1, 0); // Change as necessary
        Vector3 foodSpawnPosition = new Vector3(5, 1, 0); // Change as necessary

        // Instantiate the player and food
        _playerInstance = Instantiate(_playerPrefab, playerSpawnPosition, Quaternion.identity);
        _foodInstance = Instantiate(_foodPrefab, foodSpawnPosition, Quaternion.identity);
    }

    public void Start()
    {
        SpawnAllContent();
        _bGameIsPlaying=true;
    }

    public void GameOver()
    {
        ClearAllContent();
        _bGameIsPlaying = false;
        print("testa");
    }

    public void AddPoint(int arg)
    {
        _ui.AddScore(arg);
    }


}
