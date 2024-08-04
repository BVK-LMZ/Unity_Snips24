using UnityEngine;
using System.Collections;
public class Target : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // Reference to the Particle System
    public Material newMaterial; // Reference to the new material to apply
    private Material originalMaterial; // To store the original material

    private Renderer targetRenderer; // Reference to the Renderer component

    void Start()
    {
        // Get the Renderer component
        targetRenderer = GetComponent<Renderer>();

        // Store the original material
        if (targetRenderer != null)
        {
            originalMaterial = targetRenderer.material;
        }
    }

    void OnMouseDown()
    {
        // Handle the target click
        Debug.Log("Target clicked!");

        // Change the material if the newMaterial is assigned
        if (targetRenderer != null && newMaterial != null)
        {
            targetRenderer.material = newMaterial;
            Debug.Log("Material changed.");
        }

        // Play the Particle System if it's assigned
        if (myParticleSystem != null)
        {
            myParticleSystem.Play();
            Debug.Log("Particle System is playing.");
        }

        // Start the coroutine to destroy the game object after 0.5 seconds
        StartCoroutine(DestroyAfterDelay(0.5f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the game object
        Destroy(gameObject);
        Debug.Log("Target destroyed.");
    }
}
