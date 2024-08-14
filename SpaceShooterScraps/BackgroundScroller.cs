using UnityEngine;
using System.Collections; // for coroutines

    public class BackgroundScroller : MonoBehaviour
    { 
        [Header("Background Settings")]    
        [SerializeField] float _theScrollSpeed = 1f;// How fast the background moves upwards                                            
        [SerializeField] float _theSmoothingRate = 10f; // MovementSmoothness is (higher values mean smoother movement)                                 
        [SerializeField] float _theResetTime = 15f; // Time in seconds : after which the background position will reset

        private Vector3 _THEstartPosition;         // Initial position of the background, used in function logic()

    private IEnumerator ResetPositionCoroutine()
     // Coroutine to reset the background position after a specified time,Using a coroutine Reset the background
    {    
       yield return new WaitForSeconds(_theResetTime);
       ResetBackground();       
    }




    private void MoveBackground()
    // Moves the background upwards every frame
        {
            float newY = transform.position.y + _theScrollSpeed * Time.deltaTime; //Move background @ the scroll speed in corolation to the background
            Vector3 targetPosition = new Vector3(transform.position.x, newY, transform.position.z); // The transform {xyz} of a game object which is contantly changed in regards to the previous line
            transform.position = Vector3.Lerp(transform.position, targetPosition, _theSmoothingRate * Time.deltaTime); //Update the Vector3, with the new Y
        }

 
        private void ResetBackground()
        // Resets the background position and randomly flips its scale
        {
        //Reset Postion and the see if -Y or +Y
        transform.position = _THEstartPosition;


            if (Random.value < 0.5f)      // 50% chance to flip the scale on the Y-axis
        {
            // Ensure the Negative Y scale -Y
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);  // Ensure the Negative Y scale -Y
        }
            else
            {
            // Ensure the Postive Y scale Y
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z); // Ensure the Postive Y scale Y
        }

        }


    ///
    //MB
    ///

    void Start()
    {
        _THEstartPosition = transform.position; // Store the initial position of the background

        StartCoroutine(ResetPositionCoroutine());// Start the coroutine to reset the background position after a certain time
    }

    // Update is called once per frame
    void Update()
    {
        // Move the background upwards
        MoveBackground();
    }

}
