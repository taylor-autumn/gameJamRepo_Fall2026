using UnityEngine;
using UnityEngine.SceneManagement;

public class itemInteraction : MonoBehaviour
{

    [Header("Door stuff")]
    public bool canTryDoor = false;
    public GameObject childOfTargetDoor;

    [Header("Interact Box")]
    public GameObject interactBox;

    [Header("Player Controller Ref")]
    public PlayerControl playerControl;

    [Header("Audio Sources")]
    public AudioSource correctSound;
    public AudioClip MetalDoorSound;
    public AudioClip pickupSound;

    [Header("Computer Stuff")]
    public bool canOpenLaptop = false;
    public bool decoded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactBox.SetActive(false);
        if (playerStatus.instance.compUi == null) Debug.LogError("Comp UI is null");
        playerControl = GetComponent<PlayerControl>();
        if (playerControl == null)
        {
            Debug.LogError("PLAYER CONTROL IS NULL FROM ITEM INTERACTION SCRIPT");
        }
        playerStatus.instance.inputText.interactable = true;

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
                Destroy(door.GetComponent<BoxCollider2D>());
                thatDoorInfo.isOpen = true;
                print("DID THIS");
                correctSound.PlayOneShot(MetalDoorSound);
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
        if (Input.GetKeyDown(KeyCode.E) && canOpenLaptop)
        {
            print("opening comp");
            playerStatus.instance.compUi.SetActive(true);
            playerStatus.instance.inputText.ActivateInputField();
            playerControl.StopEveryMove(); //no move
        }
        if (Input.GetKeyDown(KeyCode.Q) && playerStatus.instance.compUi.activeInHierarchy)
        {
            print("Closing comp");
            playerStatus.instance.compUi.SetActive(false);
            playerControl.DisableStopEveryMove(); //can move again
        }
        if (Input.GetKeyDown(KeyCode.Return) && playerStatus.instance.compUi.activeInHierarchy)
        {
            print("inputting password");
            checkPassword();
        }

    }

    public string getSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return sceneName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //=================================ITEMS=================================
        //if collides with keycard
        if (collision.CompareTag("keyCard"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up keycard");
            playerStatus.instance.hasKeyCard = true;
            playerStatus.instance.updateUi(); //update ui;
            correctSound.PlayOneShot(pickupSound);
        }
        //if collides with screwdriver
        if (collision.CompareTag("screwdriver"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up screwedriver");
            playerStatus.instance.hasScrewdriver = true;
            playerStatus.instance.updateUi(); //update ui;
            correctSound.PlayOneShot(pickupSound);
        }
        //if collides with wrench
        if (collision.CompareTag("wrench"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up wrench");
            playerStatus.instance.hasWrench = true;
            playerStatus.instance.updateUi(); //update ui;
            correctSound.PlayOneShot(pickupSound);
        }
        //if collides with flashlight
        if (collision.CompareTag("flashlight"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up flashlight");
            playerStatus.instance.hasFlashlight = true;
            playerStatus.instance.updateUi(); //update ui;
            correctSound.PlayOneShot(pickupSound);
        }
        //if collides with usb
        if (collision.CompareTag("usb"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up usb");
            playerStatus.instance.hasUSB = true;
            playerStatus.instance.updateUi(); //update ui;
            correctSound.PlayOneShot(pickupSound);
        }

        //=================================collide with areas stuff==============================
        //computer
        if (collision.CompareTag("computer"))
        {
            print("in comp");
            interactBox.SetActive(true);
            canOpenLaptop = true;
        }
        if (playerStatus.instance.hasKeyCard)
        {

            //print("In door/area");
            if (collision.CompareTag("door"))
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            if (collision.CompareTag("WireBox")||collision.CompareTag("vent"))
            {
                interactBox.SetActive(true);
            }
            //open screwdriver door
            if (collision.CompareTag("screwDoor") && playerStatus.instance.hasScrewdriver)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //open flashlight level door
            if (collision.CompareTag("flashDoor") && playerStatus.instance.hasFlashlight)
            {
                print("called");
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //open wrench level door
            if (collision.CompareTag("wrenchDoor") && playerStatus.instance.hasWrench)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //leave the wrench boss level
            if (collision.CompareTag("leaveWrenchDoor") && playerStatus.instance.completedWrenchLevel)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //leave the screwdriver boss level
            if (collision.CompareTag("leaveScrewDoor") && playerStatus.instance.completedScrewLevel)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //leave flashlight boss level
            if (collision.CompareTag("leaveFlashDoor") && playerStatus.instance.completedFlashlightLevel)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //leave usb boss level
            if (collision.CompareTag("leaveUSBDoor") && playerStatus.instance.hasUSB == true)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }

        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("left door/area");
        if (collision.CompareTag("door") || collision.CompareTag("screwDoor") ||
            collision.CompareTag("flashDoor") || collision.CompareTag("wrenchDoor") ||
            collision.CompareTag("leaveWrenchDoor") || collision.CompareTag("leaveScrewDoor") ||
            collision.CompareTag("leaveUSBDoor") || collision.CompareTag("leaveFlashDoor"))
        {
            canTryDoor = false;
            childOfTargetDoor = null;
            interactBox.SetActive(false);
        }
        //computer
        if (collision.CompareTag("computer"))
        {
            print("out comp");
            canOpenLaptop = false;
            interactBox.SetActive(false);
        }
        if (collision.CompareTag("WireBox")||collision.CompareTag("vent"))
            {
                interactBox.SetActive(false);
            }
    }

    public void checkPassword()
    {
        if (playerStatus.instance.inputText.text.Trim() == playerStatus.instance.code)
        {
            print("Password Correct!");
            playerStatus.instance.inputText.interactable = false; // Disable the input field component
            playerStatus.instance.passwordText.color = Color.green;
            playerStatus.instance.passwordText.text = "Code successfully inputted!";
            playerStatus.instance.inputText.textComponent.color = Color.green;
            decoded = true;
            if (correctSound!=null) correctSound.Play();
            playerStatus.instance.completedFlashlightLevel = true;
            GoalLight.codeCompleted = true;

            //CORRECT SOUND HERE=====================================================
            //LEAH THIS IS WHERE I MAKE IT CORRECT========================================
        }
        else
        {
            print("Password Incorrect! Typed: " + playerStatus.instance.inputText.text);
        }
    }
}
