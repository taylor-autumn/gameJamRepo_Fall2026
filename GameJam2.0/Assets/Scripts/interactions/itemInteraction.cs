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
    public bool notCodeLevel=true;
    public bool canOpenLaptop = false;
    public GameObject compUi;
    public TMP_Text passwordText;
    public TMP_InputField inputText;
    public string code;
    public bool decoded = false;

    [Header("Interact Box")]
    public GameObject interactBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactBox.SetActive(false);
        if (compUi!=null) compUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (getSceneName() == "codeLevel" && notCodeLevel)
        {
            notCodeLevel = false;
            compUi = GameObject.Find("codeLevel");
            if (compUi == null)
            {
                Debug.LogError("can't find computer UI");
            }
        }

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
        if (Input.GetKeyDown(KeyCode.E) && canOpenLaptop)
        {
            print("opening comp");
            compUi.SetActive(true);
            inputText.ActivateInputField();
        }
        if (Input.GetKeyDown(KeyCode.Q) && compUi.activeInHierarchy)
        {
            print("Closing comp");
            compUi.SetActive(false);
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
