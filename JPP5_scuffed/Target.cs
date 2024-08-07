using UnityEngine;

public class Target : MonoBehaviour
{
    public ScoreTracker someScoreTracker;
    public int _pointValue = 5;
    public ParticleSystem _ps;

    private void Start()
    {
        // Get the ScoreTracker component from the Canvas
        someScoreTracker = GameObject.Find("Canvas").GetComponent<ScoreTracker>();
    }

    public void DestroyTarget()
    {
        // Instantiate and play the particle system
        ParticleSystem psInstance = Instantiate(_ps, transform.position, Quaternion.identity);
        psInstance.Play();

        // Update the score
        someScoreTracker.UpdateScore(_pointValue);

        // Check if the object has the "Bad" tag and update missed targets if so
        if (gameObject.CompareTag("Bad"))
        {
            Debug.Log("Clicked bomb");
            someScoreTracker._targetsMissed++;
        }

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
