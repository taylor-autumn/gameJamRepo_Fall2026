using UnityEngine;

public class playerStatus : MonoBehaviour
{

    public static playerStatus instance;

    [Header("Item bools")]
    public bool hasKeyCard = false;
    public bool hasScrewdriver = false;
    public bool hasWrench = false;
    public bool hasFlashlight = false;
    public bool hasUSB = false;

    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
