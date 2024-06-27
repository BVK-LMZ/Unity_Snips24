using UnityEngine;
using static SomeObject;

public class PlayerCol : MonoBehaviour
{
    public UI_Controller localUI;

    private void Start()
    {
        // Find the MainCamera in the scene
        GameObject mainCameraObj = Camera.main.gameObject;
        // Get the UI_Controller component attached to the MainCamera
        localUI = mainCameraObj.GetComponent<UI_Controller>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        // Destroy the collided game object after collision
        Destroy(other.gameObject);

        // Get the component SomeObject attached to the collided GameObject
        SomeObject someObject = other.GetComponent<SomeObject>();

        if (someObject != null)
        {
            SomeObject.ObjectType objType = someObject.objectType;


            // Handle different object types using a switch statement
            switch (objType)
            {
                case ObjectType.CheeseSlice:
                    // Handle ObjectType.SomeObjectTypeA
                    Debug.Log("Collided with SomeObjectTypeA");
                    localUI.UpdateCoins();
                    break;
                case ObjectType.MouseTrap:
                    // Handle ObjectType.SomeObjectTypeB
                    Debug.Log("Collided with SomeObjectTypeB");
                    localUI.player_dead();
                    break;
                default:
                    Debug.LogWarning("Unknown object type encountered");
                    break;
            }
        }
    }

    
}
