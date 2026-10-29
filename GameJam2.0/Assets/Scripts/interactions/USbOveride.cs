using UnityEngine;

public class USbOveride : MonoBehaviour
{
    //leah spagehhet
    public Camera NewCamera;
    public Camera DialCam;
    public Camera oldCamera;
    public Animator finale;
    public Animator lazyTransition;
    public Animator stopExploding;
    public AudioSource myAudioSource;
    public AudioClip shutDownnoise;
    public GameObject DimensionMask;
    public bool onlyOnce;
    //Char transitions
    public GameObject endingTriggerCollider;
    void Start()
    {
        onlyOnce = true;
        DialCam.enabled = false;
        NewCamera.enabled = false;
        oldCamera.enabled = true;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E)
        && GoalLight.codeCompleted == true 
        && GoalLight.ventCompleted == true
        && GoalLight.pipeCompleted == true
        && playerStatus.instance.hasUSB == true
        && onlyOnce == true)
        { //player press E and has screwdriver then okay
                print("E pressed");
                GoalLight.usbCompleted = true;
                DimensionMask.GetComponent<DimensionKey>().stopMaskShift = true;
                DimensionMask.GetComponent<DimensionKey>().maxScale = 200;
                myAudioSource.PlayOneShot(shutDownnoise);
                lazyTransition.SetTrigger("go");
                Invoke("smallDelay", 1f);
                onlyOnce = false;
        }
    }

    void smallDelay()
    {
        NewCamera.enabled = true;
        oldCamera.enabled = false;
        finale.SetBool("go", true);
        stopExploding.SetBool("go", true);
        Invoke("finaleDone", 7f);
    }

    void finaleDone()
    {
        endingTriggerCollider.SetActive(true);
        NewCamera.enabled = false;
        DialCam.enabled = true;
        print("done");
    }
}
