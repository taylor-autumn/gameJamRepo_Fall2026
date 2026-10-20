using UnityEngine;

public class itemInteraction : MonoBehaviour
{

    [Header("Door stuff")]
    public bool canTryDoor = false;
    public GameObject childOfTargetDoor;

    private void Awake()
    {
        // Keeps this GameObject alive when changing scenes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerStatus.instance.hasKeyCard && canTryDoor)
        {
            Transform door = childOfTargetDoor.transform.parent;
            doorInfo thatDoorInfo = door.GetComponent<doorInfo>();

            if (thatDoorInfo.isOpen == false)
            {
                print("opened door: " + door);
                door.GetComponent<SpriteRenderer>().color = Color.green;
                thatDoorInfo.isOpen = true;
            }
            else if (thatDoorInfo.isOpen == true)
            {
                Debug.LogWarning("That door is already open");
            }
            else
            {
                Debug.LogError("Something went wrong in calculating the door");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collides with keycard
        if (collision.CompareTag("keyCard"))
        {
            Destroy(collision.gameObject);
            print("Picked up keycard");
            playerStatus.instance.hasKeyCard = true;
        }

        //if in door area
        if (collision.CompareTag("door"))
        {
            canTryDoor = true;
            print("In door");
            childOfTargetDoor = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if in door area
        if (collision.CompareTag("door"))
        {
            childOfTargetDoor = null;
            canTryDoor = false;
            print("leave door");
        }
    }

}
