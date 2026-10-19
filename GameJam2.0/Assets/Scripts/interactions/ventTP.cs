using UnityEngine;

public class ventTP : MonoBehaviour
{
    public GameObject player;
    public Transform teleports;
    public bool canGo;
    public Animator transitionAnimator;
    public AudioClip ventAudioClip;
    private AudioSource myAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<tempVentPipe>().hasWrench == true)
        { //player press E and has wrench then okay teleport

            if (Input.GetKey(KeyCode.E))
            {
                print("E pressed");
                transitionAnimator.SetTrigger("go");
                myAudioSource.PlayOneShot(ventAudioClip);
                Invoke("smallDelay", 1f);
                player.GetComponent<tempVentPipe>().hasWrench = false;
            }
        }
    }

    void smallDelay()
    {
        player.transform.position = teleports.position;
        player.GetComponent<tempVentPipe>().hasWrench = true;
    }
}
