using UnityEngine;
using System.Collections;

public class WeaponAnim : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // Check if the left mouse button (button 0) is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Trigger the "Shoot" animation
            animator.SetTrigger("Shoot");
            // Start the coroutine to reset the trigger after 1 second
            StartCoroutine(ResetShootTriggerAfterDelay(.5f));
        }
    }

    IEnumerator ResetShootTriggerAfterDelay(float delay)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(delay);
        // Reset the "Shoot" trigger (optional, depending on your Animator setup)
        animator.ResetTrigger("Shoot");
    }
}
