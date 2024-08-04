using UnityEngine;
using System.Collections.Generic;

public class MissileLogic : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;
    private bool homing = true;

    void Start()
    {
        Destroy(gameObject, aliveTimer); // Destroy rocket after some time
        AssignRandomTarget(); // Assign a random target on start

     
    }

    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void AssignRandomTarget()
    {
        // Get all GameObjects with the "Tango" tag
        GameObject[] tangoTargets = GameObject.FindGameObjectsWithTag("Tango");

        // Ensure we have targets to assign
        if (tangoTargets.Length == 0) return;

        // Choose a random Tango target
        GameObject randomTango = tangoTargets[Random.Range(0, tangoTargets.Length)];
        target = randomTango.transform;
        // Homing is enabled by default
    }

    void OnCollisionEnter(Collision col)
    {
        if (target != null && col.gameObject.CompareTag(target.tag))
        {
            Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse); // Apply force to the target
            }
            Destroy(gameObject);
        }
    }
}
