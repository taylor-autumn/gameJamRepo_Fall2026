using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;
using static UnityEngine.Rendering.DebugUI;

public class itemInteraction : MonoBehaviour
{

    [Header("Door stuff")]
    public bool canTryDoor = false;
    public GameObject childOfTargetDoor;

    [Header("Computer stuff")]
    public bool canOpenLaptop = false;
    public GameObject compUi;
    public TMP_Text passwordText;
    public TMP_InputField inputText;
    public string code;
    public bool decoded = false;

    [Header("Interact Box")]
    public GameObject interactBox;

    [Header("Player Controller Ref")]
    public PlayerControl playerControl;
    public playerStatus playerStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactBox.SetActive(false);
        if (compUi!=null) compUi.SetActive(false);
        playerControl=GetComponent<PlayerControl>();
        if (playerControl == null)
        {
            Debug.LogError("PLAYER CONTROL IS NULL FROM ITEM INTERACTION SCRIPT");
        }

        compUi=playerStatus.compUi;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerStatus.instance.hasKeyCard && canTryDoor)
        {

            //use anim??

            Transform door = childOfTargetDoor.transform.parent;
            doorInfo thatDoorInfo = door.GetComponent<doorInfo>();

            if (thatDoorInfo.isOpen == false)
            {
                print("opened door: " + door);
                door.GetComponent<BoxCollider2D>().enabled = false;
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
        if (Input.GetKeyDown(KeyCode.E) && canOpenLaptop)
        {
            print("opening comp");
            compUi.SetActive(true);
            inputText.ActivateInputField();
            playerControl.StopEveryMove(); //no move
        }
        if (Input.GetKeyDown(KeyCode.Q) && compUi.activeInHierarchy)
        {
            print("Closing comp");
            compUi.SetActive(false);
            playerControl.DisableStopEveryMove(); //can move again
        }
        if (Input.GetKeyDown(KeyCode.Return) && compUi.activeInHierarchy)
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
        }
        //if collides with screwdriver
        if (collision.CompareTag("screwdriver"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up screwedriver");
            playerStatus.instance.hasScrewdriver = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with wrench
        if (collision.CompareTag("wrench"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up wrench");
            playerStatus.instance.hasWrench = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with flashlight
        if (collision.CompareTag("flashlight"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up flashlight");
            playerStatus.instance.hasFlashlight = true;
            playerStatus.instance.updateUi(); //update ui;
        }
        //if collides with usb
        if (collision.CompareTag("usb"))
        {
            //pick up anim??
            Destroy(collision.gameObject);
            print("Picked up usb");
            playerStatus.instance.hasUSB = true;
            playerStatus.instance.updateUi(); //update ui;
        }

        //=================================collide with areas stuff==============================
        //if in door area
        if (collision.CompareTag("door") || collision.CompareTag("area") || collision.CompareTag("computer"))
        {
            //print("In door/area");
            if (collision.CompareTag("door") && playerStatus.instance.hasKeyCard)
            {
                interactBox.SetActive(true);
                canTryDoor = true;
                childOfTargetDoor = collision.gameObject;
            }
            //computer
            if (collision.CompareTag("computer"))
            {
                print("in comp");
                interactBox.SetActive(true);
                canOpenLaptop = true;
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
        //computer
        if (collision.CompareTag("computer"))
        {
            print("out comp");
            canOpenLaptop = false;
        }
    }

    public void checkPassword()
    {
        if (inputText.text.Trim() == code)
        {
            print("Password Correct!");
            inputText.interactable = false; // Disable the input field component
            passwordText.color = Color.green;
            passwordText.text = "Code successfully inputted!";
            inputText.textComponent.color = Color.green;
            decoded = true;

            //CORRECT SOUND HERE=====================================================
            //LEAH THIS IS WHERE I MAKE IT CORRECT========================================
        }
        else
        {
            print("Password Incorrect! Typed: " + inputText.text);
        }
    }
}
