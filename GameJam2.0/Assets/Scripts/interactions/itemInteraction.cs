using UnityEngine;

public class itemInteraction : MonoBehaviour
{
    public bool hasKeyCard = false;
    public bool canTryDoor = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasKeyCard && canTryDoor)
        {
            print("openDoor");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collides with keycard
        if (collision.CompareTag("keyCard"))
        {
            Destroy(collision.gameObject);
            print("Picked up keycard");
            hasKeyCard = true;
        }

        //if in door area
        if (collision.CompareTag("door"))
        {
            canTryDoor = true;
            print("In door");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if in door area
        if (collision.CompareTag("door"))
        {
            canTryDoor = false;
            print("leave door");
        }
    }

}
