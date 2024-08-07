using UnityEngine;

public class Floor : MonoBehaviour
{

    public BoxCollider _bc;
    public ScoreTracker tracker;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit other:" + other);
        if (other.CompareTag("Good"))
        {
            tracker.MissedTarget();
        }
    }
}
