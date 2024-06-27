using UnityEngine;
using UnityEngine.Events;

public class SomeObject : MonoBehaviour
{
    // Enum to represent different object types
    public enum ObjectType
    {
        CheeseSlice,
        MouseTrap
    }



    // Prefabs for CheeseSlice and MouseTrap
    public GameObject cheeseSlicePrefab;
    public GameObject mouseTrapPrefab;

    // Time before destroying the spawned object
    public float timeToDestroy = 15f;

    public ObjectType objectType { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
     



        // Randomly choose between CheeseSlice and MouseTrap
        ObjectType randomType = (Random.Range(0, 2) == 0) ? ObjectType.CheeseSlice : ObjectType.MouseTrap;

        // Initialize object based on the random type
        GameObject obj = null;
        string objName = "";

        switch (randomType)
        {
            case ObjectType.CheeseSlice:
                obj = Instantiate(cheeseSlicePrefab, transform.position, Quaternion.identity);
                this.objectType = ObjectType.CheeseSlice;
                objName = "SomeCheese";
                break;
            case ObjectType.MouseTrap:
                obj = Instantiate(mouseTrapPrefab, transform.position, Quaternion.identity);
                this.objectType = ObjectType.MouseTrap;
                objName = "SomeMouseTrap";
                break;
        }

        if (obj != null)
        {
            obj.transform.parent = this.transform; // Set the parent to the object that spawned it
            DestroyAfterTime(obj); // Destroy the object after `timeToDestroy` seconds
        }
    }

  

    private void Update()
    {
        // Move left
        transform.position += Vector3.left * Time.deltaTime;
    }

    private void DestroyAfterTime(GameObject obj)
    {
        // Disable collider first to prevent interactions
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        // Destroy the parent object and all of its children after `timeToDestroy` seconds
        Destroy(gameObject, timeToDestroy);
    }
}
