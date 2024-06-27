using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private UI _gameUI;
    [SerializeField] private GameModeMain _gameModeMain;

    private void Start()
    {

        _gameModeMain = GameObject.Find("MainCamera")?.GetComponent<GameModeMain>();


        RandomizePosition();

        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void RandomizePosition()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float randomX = Random.Range(mainCamera.transform.position.x - cameraWidth / 2 + _boxCollider.size.x / 2, mainCamera.transform.position.x + cameraWidth / 2 - _boxCollider.size.x / 2);
        float randomY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2 + _boxCollider.size.y / 2, mainCamera.transform.position.y + cameraHeight / 2 - _boxCollider.size.y / 2);

        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player ATE FOOD");
            _gameModeMain.AddPoint(1);
            RandomizePosition();
        }

        
       
    }
}
