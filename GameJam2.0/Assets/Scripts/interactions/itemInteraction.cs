using UnityEngine;

public class itemInteraction : MonoBehaviour
{

    [Header("Door stuff")]
    public bool canTryDoor = false;
    public GameObject childOfTargetDoor;

    [Header("Interact Box")]
    public GameObject interactBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactBox.SetActive(false);
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
        //=================================ITEMS=================================
        //if collides with keycard
        if (collision.CompareTag("keyCard"))
        {
            Destroy(collision.gameObject);
            print("Picked up keycard");
            playerStatus.instance.hasKeyCard = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with screwdriver
        if (collision.CompareTag("screwdriver"))
        {
            Destroy(collision.gameObject);
            print("Picked up screwedriver");
            playerStatus.instance.hasScrewdriver = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with wrench
        if (collision.CompareTag("wrench"))
        {
            Destroy(collision.gameObject);
            print("Picked up wrench");
            playerStatus.instance.hasWrench = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with flashlight
        if (collision.CompareTag("flashlight"))
        {
            Destroy(collision.gameObject);
            print("Picked up flashlight");
            playerStatus.instance.hasFlashlight = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with usb
        if (collision.CompareTag("usb"))
        {
            Destroy(collision.gameObject);
            print("Picked up usb");
            playerStatus.instance.hasUSB = true;
            playerStatus.instance.updateUi(); //update ui;
        }

        //=================================door stuff==============================
        //if in door area
        if (collision.CompareTag("door") || collision.CompareTag("area"))
        {
            //print("In door/area");
            interactBox.SetActive(true);
            if (collision.CompareTag("door"))
            {
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("left door/area");
        interactBox.SetActive(false);
        if (collision.CompareTag("door"))
        {
            canTryDoor = false;
            childOfTargetDoor = null;
        }
    }

}
