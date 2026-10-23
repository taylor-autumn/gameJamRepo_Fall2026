using UnityEngine;
using UnityEngine.UI;

public class playerStatus : MonoBehaviour
{

    public static playerStatus instance;

    [Header("Item bools")]
    public bool hasKeyCard = false;
    public bool hasScrewdriver = false;
    public bool hasWrench = false;
    public bool hasFlashlight = false;
    public bool hasUSB = false;

    [Header("Ui Stuff")]
    public Image keyCardUi;
    public Image screwdriverUi;
    public Image wrenchUi;
    public Image flashlightUi;
    public Image usbUi;
    public GameObject compUi;

    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject); //this object should never die

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //singleton reference
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        //bools initialize
        hasKeyCard = false;
        hasScrewdriver = false;
        hasWrench = false;
        hasFlashlight = false;
        hasUSB = false;

        //initialize ui
        updateUi();
    }

    // Update is called once per frame
    void Update()
    {
        //cheat ui
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            updateUi();
        }


    }

    public void updateUi()
    {
        //keycard
        if (hasKeyCard)
        {
            keyCardUi.color = Color.white;
        }
        else
        {
            keyCardUi.color = Color.black;
        }
        //screwdriver
        if (hasScrewdriver)
        {
            screwdriverUi.color = Color.white;
        }
        else
        {
            screwdriverUi.color = Color.black;
        }
        //wrench
        if (hasWrench)
        {
            wrenchUi.color = Color.white;
        }
        else
        {
            wrenchUi.color = Color.black;
        }
        //flashlight
        if (hasFlashlight)
        {
            flashlightUi.color = Color.white;
        }
        else
        {
            flashlightUi.color = Color.black;
        }
        //usb
        if (hasUSB)
        {
            usbUi.color = Color.white;
        }
        else
        {
            usbUi.color = Color.black;
        }

    }
}
