using UnityEngine;

public class USbOveride : MonoBehaviour
{
    public Camera NewCamera;
    public Camera oldCamera;
    public Animator finale;
    public Animator lazyTransition;
    public Animator stopExploding;
    private AudioSource myAudioSource;
    public AudioClip shutDownnoise;
    public GameObject DimensionMask;
    void Start()
    {
        NewCamera.enabled = false;
        oldCamera.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E) &&
        playerStatus.instance.hasScrewdriver == true)
        { //player press E and has screwdriver then okay
                print("E pressed");
                DimensionMask.GetComponent<DimensionKey>().stopMaskShift = true;
                DimensionMask.GetComponent<DimensionKey>().maxScale = 200;
                myAudioSource.PlayOneShot(shutDownnoise);
                lazyTransition.SetTrigger("go");
                Invoke("smallDelay", 1f);
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
        print("done");
    }
}
