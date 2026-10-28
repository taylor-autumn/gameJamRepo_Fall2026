using UnityEngine;

public class ventTP : MonoBehaviour
{
    public GameObject player;
    public Transform teleports;
    public Animator transitionAnimator;
    public Animator ventingit;
    public AudioClip ventAudioClip;
    private AudioSource myAudioSource;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E) &&
        playerStatus.instance.hasScrewdriver == true)
        { //player press E and has screwdriver then okay
                print("E pressed");
                transitionAnimator.SetTrigger("go");
                ventingit.SetTrigger("go");
                myAudioSource.PlayOneShot(ventAudioClip);
                Invoke("smallDelay", 1f);
        }
    }
    void smallDelay()
    {
        player.transform.position = teleports.position;
    }
}
