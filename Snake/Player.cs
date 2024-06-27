using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _direction = Vector2.right;
    [SerializeField] private Vector2 _lastDirection = Vector2.right;
    [SerializeField] private List<Transform> _segmentsList;
    [SerializeField] private Transform _segmentPrefab;
    [SerializeField] private GameModeMain _gameModeMain;    
    private void Start()
    {
        _segmentsList = new List<Transform>(); // Create a list
        _segmentsList.Add(transform); // Add the player's transform to the segments list
        _gameModeMain = GameObject.Find("MainCamera")?.GetComponent<GameModeMain>();

    }

    private void Update()
    {
        HandlePlayerInput();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _lastDirection != Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _lastDirection != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _lastDirection != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _lastDirection != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }

    private void HandlePlayerMovement()
    {
        for (int i = _segmentsList.Count - 1; i > 0; i--)
        {
            _segmentsList[i].position = _segmentsList[i - 1].position;
        }

        Vector3 newPosition = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y,
            0.0f // Keep z position at 0 since this is a 2D game
        );

        _lastDirection = _direction;
        transform.position = newPosition;

    }

    private void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _segmentsList[_segmentsList.Count - 1].position;
        _segmentsList.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Food"))
        {
            Debug.Log("Collided with Food");
            Grow();
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Collided with Player");
            Time.timeScale = 0f;
            _gameModeMain.GameOver();
            //more here
        }
    }

}
