using UnityEngine;

public class Tango : MonoBehaviour
{
    public float _speed;
    public Rigidbody _rb;
    public GameObject _playerReff;



    private void Start()
    {
        GetPlayer();
    }

    private void GetPlayer()
    {
        //get the object with the player tag
        // Find the GameObject with the t4ag "Player"
        _playerReff = GameObject.FindWithTag("Player");

        // Optionally, check if the player was found
        if (_playerReff == null)
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }
    }

    private void TangoMovement()
    {
        Vector3 lookDirection = (_playerReff.transform.position - transform.position).normalized;

        // Debug logs
       // Debug.Log("Current Position: " + transform.position);
      //  Debug.Log("Player Position: " + _playerReff.transform.position);
      //  Debug.Log("Look Direction: " + lookDirection);

        _rb.AddForce(lookDirection * _speed);
    }

    void FixedUpdate()
    {
        TangoMovement();
       
    }
}
