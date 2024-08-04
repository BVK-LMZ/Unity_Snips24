using UnityEngine;
using System.Collections;

public class PlayerSmash : MonoBehaviour
{
    public float hangTime = 0.5f;
    public float smashSpeed = 10f;
    public float explosionForce = 500f;
    public float explosionRadius = 10f;
    public float cooldownTime = 3f;

    private bool smashing = false;
    private bool onCooldown = false;
    private float floorY;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public IEnumerator Smash()  // Ensure this method is public
    {
        if (smashing || onCooldown)
            yield break;  // Exit if already smashing or on cooldown

        smashing = true;
        onCooldown = true;

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        var tangos = GameObject.FindGameObjectsWithTag("Tango");
        foreach (var tango in tangos)
        {
            if (tango != null)
            {
                Rigidbody tangoRb = tango.GetComponent<Rigidbody>();
                if (tangoRb != null)
                {
                    tangoRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
                }
            }
        }

        smashing = false;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}
